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
    public class SessionInscriptionExamsController : ControllerBase
    {
        private readonly ISessionInscriptionExamHelper _sessionInscriptionExamHelper;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SessionInscriptionExamsController(ISessionInscriptionExamHelper sessionInscriptionExamHelper, DataContext context, IUserHelper userHelper)
        {
            _sessionInscriptionExamHelper = sessionInscriptionExamHelper;
            _context = context;
            _userHelper = userHelper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var inscription = await _context.SessionInscriptionExams
                .Include(i => i.SessionInscription!)
                .Include(i => i.SessionExam!)
                    .ThenInclude(i => i.TrainingExam!)
                .Include(i => i.SessionInscriptionExamAnswers!)
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
            var queryable = _context.SessionInscriptionExams
                        .Where(s => s.SessionInscriptionId == pagination.Id)
                        .Include(i => i.SessionInscription!)
                        .Include(i => i.SessionExam!)
                            .ThenInclude(i => i.TrainingExam!)
                        .Include(i => i.SessionInscriptionExamAnswers!)
                .AsQueryable();

            return Ok(await queryable
                .OrderByDescending(x => x.Created)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.SessionInscriptionExams
                        .Where(s => s.SessionInscriptionId == pagination.Id)
                        .Include(i => i.SessionInscription!)
                        .Include(i => i.SessionExam!)
                            .ThenInclude(i => i.TrainingExam!)
                        .Include(i => i.SessionInscriptionExamAnswers!)
                    .AsQueryable();

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(SessionInscriptionExamDTO sessionInscriptionExamDTO)
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }
            var response = await _sessionInscriptionExamHelper.ProcessSessionInscriptionExamAsync(user, sessionInscriptionExamDTO);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
    }
}
