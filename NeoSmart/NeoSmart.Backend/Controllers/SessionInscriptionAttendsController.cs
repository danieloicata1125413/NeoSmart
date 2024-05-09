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
    public class SessionInscriptionAttendsController : GenericController<SessionInscriptionAttend>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public SessionInscriptionAttendsController(IGenericUnitOfWork<SessionInscriptionAttend> unitOfWork, DataContext context, IFileStorage fileStorage, IUserHelper userHelper, IMailHelper mailHelper) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        [HttpPost]
        public override async Task<IActionResult> PostAsync(SessionInscriptionAttend _SessionInscriptionAttend)
        {
            try
            {
                var validation = await _context.SessionInscriptionAttends
                    .FirstOrDefaultAsync(x => x.SessionInscriptionId == _SessionInscriptionAttend.SessionInscriptionId);
                if (validation != null)
                {
                    return BadRequest("Ya existe una firma.");
                }
                if (!_SessionInscriptionAttend.Signature!.StartsWith("https://"))
                {
                    var Session = Convert.FromBase64String(_SessionInscriptionAttend.Signature);
                    _SessionInscriptionAttend.Signature = await _fileStorage.SaveFileAsync(Session, ".jpg", "signature");
                }
                _context.Add(_SessionInscriptionAttend);
                await _context.SaveChangesAsync();
                var SessionInscriptionAttend = await _context.SessionInscriptionAttends
                    .Where(x => x.Id == _SessionInscriptionAttend.Id!)
                    .Include(x => x.SessionInscription!)
                    .ThenInclude(x => x.Session!)
                    .ThenInclude(x => x.Training!)
                    .Include(x => x.SessionInscription!)
                    .ThenInclude(x => x.Session!)
                    .ThenInclude(x => x.User!)
                    .FirstOrDefaultAsync();
                if (SessionInscriptionAttend != null)
                {
                    var user = await _userHelper.GetUserAsync(SessionInscriptionAttend!.SessionInscription!.UserId!);
                    //Enviar certificado asistencia a correo electronico
                    if (user != null)
                    {
                        var response = _mailHelper.SendMail(user!.FullName, user.Email!,
                            $"NeoSmart - Confirmación de asistencia a la capacitación",
                            $"<h4>Hola {user!.FirstName},</h4>" +
                            $"<p>A las : {SessionInscriptionAttend!.Created!}</p>" +
                            $"<p>Se ha completado la asistencia a la capacitación: {SessionInscriptionAttend!.SessionInscription!.Session!.Training!.Description}</p>" +
                            $"<p>Sesión programada: {SessionInscriptionAttend!.SessionInscription!.Session!.DateStart} {SessionInscriptionAttend!.SessionInscription!.Session!.TimeStart}</p>" +
                            $"<p>Capacitador: {SessionInscriptionAttend!.SessionInscription!.Session!.ExistUser!}</p>" +
                            $"<b>Muchas gracias!</b>");
                    }
                }
                return Ok(_SessionInscriptionAttend);
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
            var SessionAttend = await _context.SessionInscriptionAttends
                .Include(i => i.SessionInscription!)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (SessionAttend == null)
            {
                return NotFound();
            }
            return Ok(SessionAttend);
        }
    }
}
