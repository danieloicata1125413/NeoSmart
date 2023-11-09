using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Intertfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TemporalInscriptionsController : GenericController<TemporalInscription>
    {
        private readonly DataContext _context;
        public TemporalInscriptionsController(IGenericUnitOfWork<TemporalInscription> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _context.TemporalInscriptions
                .Include(ts => ts.User!)
                .Include(ts => ts.Training!)
                .ThenInclude(p => p.TrainingTopics!)
                .ThenInclude(pc => pc.Topic)
                .Include(ts => ts.Training!)
                .ThenInclude(p => p.TrainingImages)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutAsync(TemporalInscriptionDTO temporalInscriptionDTO)
        {
            var currentTemporalInscription = await _context.TemporalInscriptions.FirstOrDefaultAsync(x => x.Id == temporalInscriptionDTO.Id);
            if (currentTemporalInscription == null)
            {
                return NotFound();
            }

            currentTemporalInscription!.Remarks = temporalInscriptionDTO.Remarks;

            _context.Update(currentTemporalInscription);
            await _context.SaveChangesAsync();
            return Ok(temporalInscriptionDTO);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostAsync(TemporalInscriptionDTO temporalInscriptionDTO)
        {
            var training = await _context.Trainings.FirstOrDefaultAsync(x => x.Id == temporalInscriptionDTO.TrainingId);
            if (training == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return NotFound();
            }

            var temporalInscription = new TemporalInscription
            {
                Training = training,
                Remarks = temporalInscriptionDTO.Remarks,
                User = user
            };

            try
            {
                _context.Add(temporalInscription);
                await _context.SaveChangesAsync();
                return Ok(temporalInscriptionDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.TemporalInscriptions
                .Include(ts => ts.User!)
                .Include(ts => ts.Training!)
                .ThenInclude(p => p.TrainingTopics!)
                .ThenInclude(pc => pc.Topic)
                .Include(ts => ts.Training!)
                .ThenInclude(p => p.TrainingImages)
                .Where(x => x.User!.Email == User.Identity!.Name)
                .ToListAsync());
        }

        [HttpGet("count")]
        public async Task<ActionResult> GetCountAsync()
        {
            return Ok(await _context.TemporalInscriptions
                .Where(x => x.User!.Email == User.Identity!.Name)
                .CountAsync());
        }
    }
}
