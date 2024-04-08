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
    public class TopicsController : GenericController<Topic>
    {
        private readonly DataContext _context;

        public TopicsController(IGenericUnitOfWork<Topic> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
            _context = context;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Topics
                                .Include(t => t.FormationTopics)
                                .Include(t => t.TrainingTopics)
                                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Description)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Topics
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var topic = await _context.Topics
                .Include(t => t.FormationTopics)
                .Include(t => t.TrainingTopics)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }
    }
}
