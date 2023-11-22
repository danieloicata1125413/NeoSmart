using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helpers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : GenericController<Slider>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public SlidersController(IGenericUnitOfWork<Slider> unitOfWork, DataContext context, IFileStorage fileStorage) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetComboAsync()
        {
            return Ok(await _context.Sliders
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Sliders
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(c => c.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Sliders.AsQueryable();

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
            var process = await _context.Sliders
                .FirstOrDefaultAsync(c => c.Id == id);
            if (process == null)
            {
                return NotFound();
            }
            return Ok(process);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(SliderDTO SliderDTO)
        {
            try
            {
                Slider newSlider = new()
                {
                    Description = SliderDTO.Description,
                };
                var photoTraining = Convert.FromBase64String(SliderDTO.NewImage!);
                newSlider.Image = await _fileStorage.SaveFileAsync(photoTraining, ".jpg", "sliders");
                _context.Add(newSlider);
                await _context.SaveChangesAsync();
                return Ok(SliderDTO);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
