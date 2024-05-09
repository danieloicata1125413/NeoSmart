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
    public class SessionInscriptionTemporalsController : GenericController<SessionInscriptionTemporal>
    {
        private readonly DataContext _context;
        public SessionInscriptionTemporalsController(IGenericUnitOfWork<SessionInscriptionTemporal> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _context.SessionInscriptionTemporals
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
        public async Task<IActionResult> PutAsync(SessionInscriptionTemporalDTO SessionInscriptionTemporalDTO)
        {
            var currentSessionInscriptionTemporal = await _context.SessionInscriptionTemporals.FirstOrDefaultAsync(x => x.Id == SessionInscriptionTemporalDTO.Id);
            if (currentSessionInscriptionTemporal == null)
            {
                return NotFound();
            }

            currentSessionInscriptionTemporal!.Remarks = SessionInscriptionTemporalDTO.Remarks;

            _context.Update(currentSessionInscriptionTemporal);
            await _context.SaveChangesAsync();
            return Ok(SessionInscriptionTemporalDTO);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostAsync(SessionInscriptionTemporalDTO SessionInscriptionTemporalDTO)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(x => x.Id == SessionInscriptionTemporalDTO.SessionId);
            if (session == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);
            if (user == null)
            {
                return NotFound();
            }

            var SessionInscriptionTemporal = new SessionInscriptionTemporal
            {
                SessionId = session.Id,
                Remarks = SessionInscriptionTemporalDTO.Remarks,
                User = user
            };

            try
            {
                _context.Add(SessionInscriptionTemporal);
                await _context.SaveChangesAsync();
                return Ok(SessionInscriptionTemporalDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _context.SessionInscriptionTemporals
                .Include(x => x.Session!)
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
            return Ok(await _context.SessionInscriptionTemporals
                .Where(x => x.User!.Email == User.Identity!.Name)
                .CountAsync());
        }
    }
}
