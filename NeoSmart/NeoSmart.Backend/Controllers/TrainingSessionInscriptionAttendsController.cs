using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Helpers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using System.Linq;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrainingSessionInscriptionAttendsController : GenericController<TrainingSessionInscriptionAttend>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public TrainingSessionInscriptionAttendsController(IGenericUnitOfWork<TrainingSessionInscriptionAttend> unitOfWork, DataContext context, IFileStorage fileStorage, IUserHelper userHelper, IMailHelper mailHelper) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        [HttpPost]
        public override async Task<IActionResult> PostAsync(TrainingSessionInscriptionAttend _trainingSessionInscriptionAttend)
        {
            try
            {
                var validation = await _context.TrainingSessionInscriptionAttends
                    .FirstOrDefaultAsync(x => x.TrainingSessionInscriptionId == _trainingSessionInscriptionAttend.TrainingSessionInscriptionId);
                if (validation != null)
                {
                    return BadRequest("Ya existe una firma.");
                }
                if (!_trainingSessionInscriptionAttend.Signature!.StartsWith("https://"))
                {
                    var trainingSession = Convert.FromBase64String(_trainingSessionInscriptionAttend.Signature);
                    _trainingSessionInscriptionAttend.Signature = await _fileStorage.SaveFileAsync(trainingSession, ".jpg", "signature");
                }
                _context.Add(_trainingSessionInscriptionAttend);
                await _context.SaveChangesAsync();
                var trainingSessionInscriptionAttend = await _context.TrainingSessionInscriptionAttends
                    .Where(x => x.Id == _trainingSessionInscriptionAttend.Id!)
                    .Include(x => x.TrainingSessionInscription!)
                    .ThenInclude(x => x.TrainingSession!)
                    .ThenInclude(x => x.Training!)
                    .Include(x => x.TrainingSessionInscription!)
                    .ThenInclude(x => x.TrainingSession!)
                    .ThenInclude(x => x.User!)
                    .FirstOrDefaultAsync();
                if (trainingSessionInscriptionAttend != null)
                {
                    var user = await _userHelper.GetUserAsync(trainingSessionInscriptionAttend!.TrainingSessionInscription!.UserId!);
                    //Enviar certificado asistencia a correo electronico
                    if (user != null)
                    {
                        var response = _mailHelper.SendMail(user!.FullName, user.Email!,
                            $"NeoSmart - Confirmación de asistencia a la capacitación",
                            $"<h4>Hola {user!.FirstName},</h4>" +
                            $"<p>A las : {trainingSessionInscriptionAttend!.Created!}</p>" +
                            $"<p>Se ha completado la asistencia a la capacitación: {trainingSessionInscriptionAttend!.TrainingSessionInscription!.TrainingSession!.Training!.Description}</p>" +
                            $"<p>Sesión programada: {trainingSessionInscriptionAttend!.TrainingSessionInscription!.TrainingSession!.DateStart} {trainingSessionInscriptionAttend!.TrainingSessionInscription!.TrainingSession!.TimeStart}</p>" +
                            $"<p>Capacitador: {trainingSessionInscriptionAttend!.TrainingSessionInscription!.TrainingSession!.ExistUser!}</p>" +
                            $"<b>Muchas gracias!</b>");
                    }
                }
                return Ok(_trainingSessionInscriptionAttend);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe una firma.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var trainingSessionAttend = await _context.TrainingSessionInscriptionAttends
                .Include(i => i.TrainingSessionInscription!)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (trainingSessionAttend == null)
            {
                return NotFound();
            }
            return Ok(trainingSessionAttend);
        }
    }
}
