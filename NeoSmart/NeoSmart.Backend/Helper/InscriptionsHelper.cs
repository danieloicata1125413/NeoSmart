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

            var temporalInscriptions = await _context.TrainingSessionInscriptionTemporals
                .Include(x => x.TrainingSession)
                .ThenInclude(x => x.Training)
                .Where(x => x.User!.Email == email)
                .ToListAsync();

            foreach (var temporalInscription in temporalInscriptions)
            {
                TrainingSessionInscription inscription = new()
                {
                    Date = DateTime.UtcNow,
                    User = user,
                    Remarks = temporalInscription.Remarks,
                    TrainingSessionId = temporalInscription.TrainingSessionId,
                    InscriptionStatus = InscriptionStatus.Registered
                };
                _context.TrainingSessionInscriptions.Add(inscription);
                //Enviar email
                var response = _mailHelper.SendMail(user.FullName, user.Email!,
                    $"NeoSmart - Nueva inscripción",
                    $"<h4>Hola {inscription.User!.FirstName},</h4>" +
                    $"<p>Se ha realizado una inscripción a la capacitación: {inscription.TrainingSession!.Training!.Description}</p>" +
                    $"<b>Muchas gracias!</b>");
                _context.TrainingSessionInscriptionTemporals.Remove(temporalInscription);
            }

            await _context.SaveChangesAsync();

            return new Response<bool>() { IsSuccess = true };
        }
    }
}
