using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrainingExamsController : GenericController<TrainingExam>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public TrainingExamsController(IGenericUnitOfWork<TrainingExam> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet("combo/{TrainingId}")]
        public async Task<ActionResult> GetComboAllAsync(int TrainingId)
        {
            return Ok(await _context.TrainingExams
                .Where(c => c.Training!.Id == TrainingId)
                .OrderBy(s => s.Training!.Description)
                .ThenBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingExams
                            .Where(x => x.Training!.Id == pagination.Id)
                            .Include(o => o.Training)
                            .Include(t => t.TrainingExamQuestions)
                            .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(s => s.Training!.Description)
                .ThenBy(s => s.Description)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingExams
                .Where(x => x.Training!.Id == pagination.Id)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            queryable = queryable
                .OrderBy(s => s.Training!.Description)
                .ThenBy(s => s.Description);
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var TrainingExam = await _context.TrainingExams
                .Include(o => o.Training!)
                .Include(t => t.TrainingExamQuestions!)
                .ThenInclude(t => t.TrainingExamQuestionOptions!)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (TrainingExam == null)
            {
                return NotFound();
            }
            return Ok(TrainingExam);
        }
    }
}
