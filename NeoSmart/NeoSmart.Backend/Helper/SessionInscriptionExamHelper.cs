using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Responses;
using NeoSmart.Data.Entities;
using System.Security.Cryptography.Xml;

namespace NeoSmart.BackEnd.Helper
{
    public class SessionInscriptionExamHelper : ISessionInscriptionExamHelper
    {
        private readonly DataContext _context;
        private readonly IMailHelper _mailHelper;

        public SessionInscriptionExamHelper(DataContext context, IMailHelper mailHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
        }
        public async Task<Response<bool>> ProcessSessionInscriptionExamAsync(User user, SessionInscriptionExamDTO sessionInscriptionExamDTO)
        {
            var sessionInscription = await _context.SessionInscriptions
                .Include(i => i.User!)
                .Include(i => i.Session!)
                    .ThenInclude(i => i.User!)
                .FirstOrDefaultAsync(x => x.Id == sessionInscriptionExamDTO.SessionInscriptionId);

            if (user.UserName != sessionInscription!.User!.UserName)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = "Usuario no válido"
                };
            }
            var sessionExam = await _context.SessionExams
                .Include(i => i.TrainingExam!)
                .ThenInclude(i => i.Training!)
                .FirstOrDefaultAsync(x => x.Id == sessionInscriptionExamDTO.SessionExamId);
            SessionInscriptionExam sessionInscriptionExam = new SessionInscriptionExam()
            {
                SessionInscriptionId = sessionInscriptionExamDTO.SessionInscriptionId,
                SessionExamId = sessionInscriptionExamDTO.SessionExamId, 
                Description = sessionInscriptionExamDTO.Description,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Status = true, 
            };
            sessionInscriptionExam.SessionInscriptionExamAnswers = new List<SessionInscriptionExamAnswer>();
            foreach (var item in sessionInscriptionExamDTO.SessionInscriptionExamAnswers!)
            {
                sessionInscriptionExam.SessionInscriptionExamAnswers.Add(new SessionInscriptionExamAnswer()
                {
                    SessionInscriptionExamId = sessionInscriptionExamDTO.SessionInscriptionId,
                    Question = item.Question,
                    Answer = item.Answer, 
                    Correct = item.Correct,
                    Created = DateTime.Now,
                    Updated = DateTime.Now, 
                    Status = true, 
                }) ;
            }
            var result = string.Empty;
            if (sessionInscriptionExam.Aprobado)

                result = "APROBADA";
            else
                result = "NO APROBADA";

            _context.SessionInscriptionExams.Add(sessionInscriptionExam);
            //Enviar email
            var response = _mailHelper.SendMail(user.FullName, user.Email!,
                $"NeoSmart - Se ha diligenciado una medición",
                $"<h4>Hola {user!.FirstName},</h4>" +
                $"<p>Se ha realizado la medición: {sessionExam!.TrainingExam!.Description}</p>" +
                $"<p>Resultado: {result}</p>" +
                $"<p>Capacitación: {sessionExam!.TrainingExam!.Training!.Description}</p>" +
                $"<p>Sesión programada: {sessionExam!.DateStart!}</p>" +
                $"<p>Instructor: {sessionInscription!.Session!.ExistUser}</p>" +
                $"<b>Muchas gracias!</b>");
            await _context.SaveChangesAsync();

            return new Response<bool>() { IsSuccess = true };
        }
    }
}
