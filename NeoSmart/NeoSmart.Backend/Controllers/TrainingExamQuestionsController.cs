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
    public class TrainingExamQuestionsController : GenericController<TrainingExamQuestion>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public TrainingExamQuestionsController(IGenericUnitOfWork<TrainingExamQuestion> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet("combo/{TrainingExamId}")]
        public async Task<ActionResult> GetComboAllAsync(int TrainingExamId)
        {
            return Ok(await _context.TrainingExamQuestions
                .Where(c => c.TrainingExam!.Id == TrainingExamId)
                .Include(c => c.TrainingExamQuestionOptions)
                .OrderBy(s => s.TrainingExam!.Description)
                .ThenBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingExamQuestions
                            .Where(x => x.TrainingExam!.Id == pagination.Id)
                            .Include(o => o.TrainingExam)
                            .Include(c => c.TrainingExamQuestionOptions)
                            .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(s => s.TrainingExam!.Description)
                .ThenBy(s => s.Description)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingExamQuestions
                .Where(x => x.TrainingExam!.Id == pagination.Id)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            queryable = queryable
                 .OrderBy(s => s.TrainingExam!.Description)
                .ThenBy(s => s.Description);
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var TrainingExamQuestion = await _context.TrainingExamQuestions
                .Include(o => o.TrainingExam)
                .Include(c => c.TrainingExamQuestionOptions)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (TrainingExamQuestion == null)
            {
                return NotFound();
            }
            return Ok(TrainingExamQuestion);
        }
    }
}
