using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Responses;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Helper
{
    public class InscriptionsHelper : IInscriptionsHelper
    {
        private readonly DataContext _context;
        private readonly IMailHelper _mailHelper;
        private readonly IUserHelper _userHelper;

        public InscriptionsHelper(DataContext context, IMailHelper mailHelper, IUserHelper userHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
            _userHelper = userHelper;
        }

        public async Task<Response<bool>> ProcessInscriptionAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Usuario no válido"
                };
            }

            var temporalInscriptions = await _context.SessionInscriptionTemporals
                .Include(x => x.User!)
                .Include(x => x.Session!)
                .ThenInclude(x => x.Training!)
                .Where(x => x.User!.Email == email)
                .ToListAsync();

            foreach (var temporalInscription in temporalInscriptions)
            {
                var sessionInscriptionStatus = await _context.SessionInscriptionStatus.FirstOrDefaultAsync(x => x.Name.Equals("Registered"));
                SessionInscription inscription = new()
                {
                    Date = DateTime.UtcNow,
                    Remarks = temporalInscription.Remarks,
                    UserId = user.Id,
                    Certificate = null,
                    SessionId = temporalInscription.SessionId,
                    SessionInscriptionStatusId = sessionInscriptionStatus!.Id,
                    Status = true,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                };
                _context.SessionInscriptions.Add(inscription);
                //Enviar email
                var response = _mailHelper.SendMail(temporalInscription.User!.FullName, temporalInscription.User.Email!,
                    $"NeoSmart - Nueva inscripción",
                    $"<h4>Hola {temporalInscription.User!.FirstName},</h4>" +
                    $"<p>Se ha realizado una inscripción a la capacitación: {temporalInscription.Session!.Training!.Description}</p>" +
                    $"<p>Sesión programada para: {temporalInscription.Session!.DateStart.ToLongDateString()}</p>" +
                    $"<b>Muchas gracias!</b>");
                _context.SessionInscriptionTemporals.Remove(temporalInscription);
            }

            await _context.SaveChangesAsync();

            return new Response<bool>() { IsSuccess = true };
        }

        public async Task<Response<bool>> ProcessInscriptionFullAsync(string email, SessionInscriptionCreateDTO sessionInscriptionCreate)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            var session = await _context.Sessions
            .Where(x => x.Id == sessionInscriptionCreate!.SesionId)
            .Include(x => x.SessionInscriptions)
            .Include(x => x.Training!)
            .FirstOrDefaultAsync();
            if (session == null)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Sesión no válida"
                };
            }
            foreach (var userId in sessionInscriptionCreate.Users!)
            {
                if (!session.SessionInscriptions!.Any(x => x.UserId == userId))
                {
                    var sessionInscriptionStatus = await _context.SessionInscriptionStatus.FirstOrDefaultAsync(x => x.Name.Equals("Confirmed"));
                    var userTemporal = await _userHelper.GetUserAsync(userId);
                    SessionInscription inscription = new()
                    {
                        Date = DateTime.UtcNow,
                        Remarks = "Inscrito",
                        UserId = userId,
                        Certificate = null,
                        SessionId = session.Id,
                        SessionInscriptionStatusId = sessionInscriptionStatus!.Id,
                        Status = true,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow,
                    };
                    _context.SessionInscriptions.Add(inscription);
                    //Enviar email
                    var response = _mailHelper.SendMail(userTemporal.FullName, userTemporal.Email!,
                        $"NeoSmart - Nueva inscripción",
                        $"<h4>Hola {userTemporal!.FirstName},</h4>" +
                        $"<p>Se ha realizado una inscripción a la capacitación: {session!.Training!.Description} por el usuario: {user!.FullName}</p>" +
                        $"<p>Sesión programada para: {session!.DateStart.ToLongDateString()}</p>" +
                        $"<b>Muchas gracias!</b>");
                }
            }
            await _context.SaveChangesAsync();

            return new Response<bool>() { IsSuccess = true };
        }

    }
}
