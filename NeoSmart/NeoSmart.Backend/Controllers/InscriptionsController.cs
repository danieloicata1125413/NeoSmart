using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
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

        public InscriptionsController(IInscriptionsHelper inscriptionsHelper, DataContext context, IUserHelper userHelper)
        {
            _inscriptionsHelper = inscriptionsHelper;
            _context = context;
            _userHelper = userHelper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var inscription = await _context.Inscriptions
                .Include(s => s.User!)
                .ThenInclude(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .Include(s => s.Training!)
                .ThenInclude(p => p.TrainingImages)
                .Include(s => s.Training!)
                .ThenInclude(p => p.TrainingTopics!)
                .ThenInclude(p => p.Topic)
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
                .Include(s => s.User)
                .Include(s => s.Training)
                .ThenInclude(sd => sd!.TrainingTopics)
                .Include(s => s.Training)
                .ThenInclude(i => i!.TrainingImages)
                .AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Administrador.ToString());
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

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Administrador.ToString());
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

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Administrador.ToString());
            if (!isAdmin && inscriptionDTO.InscriptionStatus != InscriptionStatus.Refused)
            {
                return BadRequest("Solo permitido para administradores.");
            }

            var inscription = await _context.Inscriptions
                .Include(s => s.Training)
                .FirstOrDefaultAsync(s => s.Id == inscriptionDTO.Id);

            if (inscription == null)
            {
                return NotFound();
            }

            if (inscriptionDTO.InscriptionStatus == InscriptionStatus.Confirmed)
            {
                //enviar email
            }

            if (inscriptionDTO.InscriptionStatus == InscriptionStatus.Refused)
            {
                //enviar email
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
