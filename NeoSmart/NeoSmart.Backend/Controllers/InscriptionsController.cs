using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class InscriptionsController : ControllerBase
    {
        private readonly IInscriptionsHelper _inscriptionsHelper;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public InscriptionsController(IInscriptionsHelper inscriptionsHelper, DataContext context, IUserHelper userHelper, IMailHelper mailHelper)
        {
            _inscriptionsHelper = inscriptionsHelper;
            _context = context;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var inscription = await _context.Inscriptions
                .Include(i => i.TrainingCalendar!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingImages!)
                .Include(i => i.TrainingCalendar!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingTopics!)
                    .ThenInclude(i => i.Topic!)
                .Include(i => i.TrainingCalendar!)
                  .ThenInclude(i => i.User!)
                .Include(i => i.TrainingCalendar!)
                     .ThenInclude(i => i.City!)
                     .ThenInclude(i => i.State!)
                     .ThenInclude(i => i.Country!)
                .Include(i => i.User!)
                    .ThenInclude(i => i.City!)
                    .ThenInclude(i => i.State!)
                    .ThenInclude(i => i.Country!)
                .FirstOrDefaultAsync(s => s.Id == id);




            if (inscription == null)
            {
                return NotFound();
            }
            return Ok(inscription);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var queryable = _context.Inscriptions
                .Include(i => i.TrainingCalendar!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingImages!)
                .Include(i => i.TrainingCalendar!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingTopics!)
                    .ThenInclude(i => i.Topic!)
                .Include(i => i.TrainingCalendar!)
                  .ThenInclude(i => i.User!)
                .Include(i => i.TrainingCalendar!)
                     .ThenInclude(i => i.City!)
                     .ThenInclude(i => i.State!)
                     .ThenInclude(i => i.Country!)
                .Include(i => i.User!)
                    .ThenInclude(i => i.City!)
                    .ThenInclude(i => i.State!)
                    .ThenInclude(i => i.Country!)
                .AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == User.Identity!.Name);
            }

            return Ok(await queryable
                .OrderByDescending(x => x.Date)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var queryable = _context.Inscriptions.AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == User.Identity!.Name);
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(InscriptionDTO inscriptionDTO)
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin && inscriptionDTO.InscriptionStatus != InscriptionStatus.Cancelled)
            {
                return BadRequest("Solo permitido para trabajadores.");
            }

            var inscription = await _context.Inscriptions
                .Include(s => s.User)
                .Include(s => s.TrainingCalendar!)
                .ThenInclude(s => s.Training)
                .FirstOrDefaultAsync(s => s.Id == inscriptionDTO.Id);

            if (inscription == null)
            {
                return NotFound();
            }

            if (inscriptionDTO.InscriptionStatus == InscriptionStatus.Confirmed)
            {
                //enviar email
                var response = _mailHelper.SendMail(inscription.User!.FullName, inscription.User!.Email!,
                $"NeoSmart - Confirmación de inscripción",
                $"<h4>Hola {inscription.User!.FirstName},</h4>" +
                $"<p>Se ha confirmado tu inscripción a la capacitación: {inscription.TrainingCalendar!.Training!.Description}</p>" +
                $"<b>Muchas gracias!</b>");
            }

            if (inscriptionDTO.InscriptionStatus == InscriptionStatus.Refused)
            {
                //enviar email
                var response = _mailHelper.SendMail(inscription.User!.FullName, inscription.User!.Email!,
                $"NeoSmart - Rechazo de inscripción",
               $"<h4>Hola {inscription.User!.FirstName},</h4>" +
               $"<p>Lamentablemente se ha rezachado tu inscripción a la capacitación: {inscription.TrainingCalendar!.Training!.Description}</p>" +
               $"<b>Será en una nueva oportunidad!</b>");
            }

            if (inscriptionDTO.InscriptionStatus == InscriptionStatus.Cancelled)
            {
                //enviar email
                var response = _mailHelper.SendMail(inscription.User!.FullName, inscription.User!.Email!,
                $"NeoSmart - Inscripción cancelada",
                $"<h4>Hola {inscription.User!.FirstName},</h4>" +
                $"<p>Se ha cancelado tu inscripción a la capacitación: {inscription.TrainingCalendar!.Training!.Description}</p>" +
                $"<b>Será en una nueva oportunidad!</b>");
            }

            inscription.InscriptionStatus = inscriptionDTO.InscriptionStatus;
            _context.Update(inscription);
            await _context.SaveChangesAsync();
            return Ok(inscription);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            var response = await _inscriptionsHelper.ProcessInscriptionAsync(User.Identity!.Name!);
            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }
    }
}
