using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TemporalInscriptionsController : GenericController<TrainingSessionInscriptionTemporal>
    {
        private readonly DataContext _context;
        public TemporalInscriptionsController(IGenericUnitOfWork<TrainingSessionInscriptionTemporal> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _context.TrainingSessionInscriptionTemporals
                .Include(ts => ts.User!)
                //.Include(tc => tc.TrainingCalendar!)
                //.ThenInclude(t => t.Training!)
                //.ThenInclude(tt => tt.TrainingTopics!)
                //.ThenInclude(pc => pc.Topic)
                //.Include(tc => tc.TrainingCalendar!)
                //.ThenInclude(t => t.Training!)
                //.ThenInclude(i => i.TrainingImages)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutAsync(TemporalInscriptionDTO temporalInscriptionDTO)
        {
            var currentTemporalInscription = await _context.TrainingSessionInscriptionTemporals.FirstOrDefaultAsync(x => x.Id == temporalInscriptionDTO.Id);
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
            var trainingCalendar = await _context.TrainingSessions.FirstOrDefaultAsync(x => x.Id == temporalInscriptionDTO.trainingCalendarId);
            if (trainingCalendar == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return NotFound();
            }

            var temporalInscription = new TrainingSessionInscriptionTemporal
            {
                TrainingSessionId = trainingCalendar.Id,
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
            var result = await _context.TrainingSessionInscriptionTemporals
                .Include(x => x.TrainingSession!)
                .ThenInclude(tc => tc.Training!)
                .ThenInclude(i => i.TrainingImages!)
                .Include(x => x.User!)
                .Where(x => x.User!.Email == User.Identity!.Name)
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult> GetCountAsync()
        {
            return Ok(await _context.TrainingSessionInscriptionTemporals
                .Where(x => x.User!.Email == User.Identity!.Name)
                .CountAsync());
        }
    }
}
