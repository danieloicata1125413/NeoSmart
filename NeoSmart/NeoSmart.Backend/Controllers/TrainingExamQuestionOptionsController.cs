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
    public class TrainingExamQuestionOptionsController : GenericController<TrainingExamQuestionOption>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public TrainingExamQuestionOptionsController(IGenericUnitOfWork<TrainingExamQuestionOption> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet("combo/{TrainingExamQuestionId}")]
        public async Task<ActionResult> GetComboAllAsync(int TrainingExamQuestionId)
        {
            return Ok(await _context.TrainingExamQuestionOptions
                .Where(c => c.TrainingExamQuestion!.Id == TrainingExamQuestionId)
                .OrderBy(s => s.TrainingExamQuestion!.Description)
                .ThenBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingExamQuestionOptions
                            .Where(x => x.TrainingExamQuestion!.Id == pagination.Id)
                            .Include(o => o.TrainingExamQuestion)
                            .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(s => s.TrainingExamQuestion!.Description)
                .ThenBy(s => s.Description)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingExamQuestionOptions
                .Where(x => x.TrainingExamQuestion!.Id == pagination.Id)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            queryable = queryable
                 .OrderBy(s => s.TrainingExamQuestion!.Description)
                .ThenBy(s => s.Description);
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var TrainingExamQuestionOption = await _context.TrainingExamQuestionOptions
                .Include(o => o.TrainingExamQuestion)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (TrainingExamQuestionOption == null)
            {
                return NotFound();
            }
            return Ok(TrainingExamQuestionOption);
        }
    }
}
