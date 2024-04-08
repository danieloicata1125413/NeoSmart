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
    public class OccupationsController : GenericController<Occupation>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OccupationsController(IGenericUnitOfWork<Occupation> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet("comboAll")]
        public async Task<ActionResult> GetComboAllAsync()
        {
            return Ok(await _context.Occupations
                .OrderBy(s => s.Process!.Company!.Name)
                .ThenBy(s => s.Process!.Description)
                .ThenBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet("combo")]
        public async Task<ActionResult> GetComboAsync()
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company == null)
            {
                Ok(await _context.Occupations
                .OrderBy(s => s.Process!.Company!.Name)
                .ThenBy(s => s.Process!.Description)
                .ThenBy(s => s.Description)
                .ToListAsync());
            }
            return Ok(await _context.Occupations
                .Where(c => c.Process!.Company!.Id == user.Company!.Id)
                .OrderBy(s => s.Process!.Company!.Name)
                .ThenBy(s => s.Process!.Description)
                .ThenBy(s => s.Description)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Occupations
                                .Include(o => o.Process!)
                                .ThenInclude(o => o.Company)
                                .Include(o => o.FormationOccupations)
                                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Process!.Company!.Id == user.Company!.Id);
            }
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(s => s.Process!.Company!.Name)
                .ThenBy(s => s.Process!.Description)
                .ThenBy(s => s.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Occupations
                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Process!.Company!.Id == user.Company!.Id);
            }
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            queryable = queryable
                .OrderBy(s => s.Process!.Company!.Name)
                .ThenBy(s => s.Process!.Description)
                .ThenBy(s => s.Description);
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var state = await _context.Occupations
                .Include(s => s.Process!)
                .ThenInclude(s => s.Company)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
