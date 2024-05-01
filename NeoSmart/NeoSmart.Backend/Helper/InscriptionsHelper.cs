using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Responses;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Helper
{
    public class InscriptionsHelper : IInscriptionsHelper
    {
        private readonly DataContext _context;
        private readonly IMailHelper _mailHelper;

        public InscriptionsHelper(DataContext context, IMailHelper mailHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
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
                var response = _mailHelper.SendMail(user.FullName, user.Email!,
                    $"NeoSmart - Nueva inscripción",
                    $"<h4>Hola {temporalInscription.User!.FirstName},</h4>" +
                    $"<p>Se ha realizado una inscripción a la capacitación: {temporalInscription.Session!.Training!.Description}</p>" +
                    $"<b>Muchas gracias!</b>");
                _context.SessionInscriptionTemporals.Remove(temporalInscription);
            }

            await _context.SaveChangesAsync();

            return new Response<bool>() { IsSuccess = true };
        }
    }
}
