using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ProcessesController : GenericController<Process>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public ProcessesController(IGenericUnitOfWork<Process> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet("combo/{companyId}")]
        public async Task<ActionResult> GetComboAsync(int companyId)
        {
            return Ok(await _context.Processes
                .Where(c => c.Company!.Id == companyId)
                .OrderBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet("combo")]
        public async Task<ActionResult> GetComboAllAsync()
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company == null)
            {
                return Ok(await _context.Processes
                .OrderBy(s => s.Description)
                .ToListAsync());
            }
            return Ok(await _context.Processes
                .Where(c => c.Company!.Id == user.Company!.Id)
                .OrderBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Processes
                .Include(c=> c.Company)
                .Include(c => c.Occupations)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(c => c.Company!.Name)
                .ThenBy(c => c.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Processes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            queryable = queryable.OrderBy(c => c.Company!.Name)
                        .ThenBy(c => c.Description);
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        // Consult those of the process by id

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var process = await _context.Processes
                .Include(c => c.Company)
                .Include(c => c.Occupations!)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (process == null)
            {
                return NotFound();
            }
            return Ok(process);
        }
    }
}
