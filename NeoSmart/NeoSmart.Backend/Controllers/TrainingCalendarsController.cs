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
    public class TrainingCalendarsController : GenericController<TrainingCalendar>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public TrainingCalendarsController(IGenericUnitOfWork<TrainingCalendar> unitOfWork, DataContext context, IFileStorage fileStorage) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingCalendars
                                .Include(o => o.User!)
                                .Include(o => o.Training!)
                                .Include(u => u.City)
                                .Where(x => x.Training!.Id == pagination.Id)
                                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Training!.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Training!.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.TrainingCalendars
                .Where(x => x.Training!.Id == pagination.Id)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Training!.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var trainingCalendar = await _context.TrainingCalendars
                .Include(o => o.User!)
                .Include(o => o.Training!)
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (trainingCalendar == null)
            {
                return NotFound();
            }
            return Ok(trainingCalendar);
        }

    }
}
