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
    public class SessionsController : GenericController<Session>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public SessionsController(IGenericUnitOfWork<Session> unitOfWork, DataContext context, IFileStorage fileStorage) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [HttpGet("comboByTrainingId/{trainingId}")]
        public async Task<ActionResult> GetComboAllAsync(int trainingId)
        {
            return Ok(await _context.Sessions
                .Include(ts => ts.User!)
                .Include(ts => ts.City!)
                .Include(s => s.SessionStatus!)
                .Include(ts => ts.TrainingSessionInscriptionTemporals!)
                .Include(ts => ts.TrainingSessionInscriptions!)
                .Where(ts => ts.TrainingId == trainingId)
                .OrderBy(ts => ts.DateStart)
                .ThenBy(ts => ts.TimeStart)
                .ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Sessions
                                .Include(s => s.User!)
                                .Include(s => s.Training!)
                                .Include(s => s.City!)
                                .Include(s => s.SessionStatus!)
                                .Include(ts => ts.TrainingSessionInscriptionTemporals!)
                                .Include(ts => ts.TrainingSessionInscriptions!)
                                .Where(x => x.Training!.Id == pagination.Id)
                                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Training!.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.SessionStatusId == pagination.Id);
            }

            return Ok(await queryable
                .OrderBy(x => x.Training!.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Sessions
                .Where(x => x.Training!.Id == pagination.Id)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Training!.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.SessionStatusId == pagination.Id);
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var trainingCalendar = await _context.Sessions
                .Include(o => o.User!)
                .Include(o => o.Training!)
                .Include(s => s.SessionStatus!)
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .Include(ts => ts.TrainingSessionInscriptionTemporals!)
                .Include(ts => ts.TrainingSessionInscriptions!)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (trainingCalendar == null)
            {
                return NotFound();
            }
            return Ok(trainingCalendar);
        }

    }
}
