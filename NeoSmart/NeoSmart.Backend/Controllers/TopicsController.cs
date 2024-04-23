using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
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
    public class TopicsController : GenericController<Topic>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public TopicsController(IGenericUnitOfWork<Topic> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }


        [AllowAnonymous]
        [HttpGet("combo/{companyId}")]
        public async Task<ActionResult> GetComboAllAsync(int companyId)
        {
            return Ok(await _context.Topics
                .Where(c => c.Company!.Id == companyId)
                .OrderBy(s => s.Company!.Name)
                .ThenBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet("comboByTrainingId/{trainingId}")]
        public async Task<ActionResult> GetComboByTrainingAsync(int trainingId)
        {
            return Ok(await _context.Topics
                .Include(t=> t.TrainingTopics!)
                .Where(t => t.TrainingTopics!.Any(x=>x.TrainingId == trainingId))
                .OrderBy(t => t.Description)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Topics
                                .Include(o => o.Company)
                                .Include(t => t.FormationTopics)
                                .Include(t => t.TrainingTopics)
                                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Company!.Id == user.Company!.Id);
            }
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(s => s.Company!.Name)
                .ThenBy(s => s.Description)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Topics
                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Company!.Id == user.Company!.Id);
            }
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            queryable = queryable
                .OrderBy(s => s.Company!.Name)
                .ThenBy(s => s.Description);
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var topic = await _context.Topics
                .Include(t => t.Company!)
                .Include(t => t.FormationTopics!)
                .Include(t => t.TrainingTopics!)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }
    }
}
