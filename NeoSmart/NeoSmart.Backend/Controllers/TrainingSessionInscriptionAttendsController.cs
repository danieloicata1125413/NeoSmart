using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helpers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using System.Linq;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrainingSessionInscriptionAttendsController : GenericController<TrainingSessionInscriptionAttend>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IUserHelper _userHelper;

        public TrainingSessionInscriptionAttendsController(IGenericUnitOfWork<TrainingSessionInscriptionAttend> unitOfWork, DataContext context, IFileStorage fileStorage, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
            _userHelper = userHelper;
        }

        [HttpPost]
        public override async Task<IActionResult> PostAsync(TrainingSessionInscriptionAttend trainingSessionInscriptionAttend)
        {
            try
            {
                var validation = await _context.TrainingSessionInscriptionAttends.FirstOrDefaultAsync(x=>x.TrainingSessionInscriptionId == trainingSessionInscriptionAttend.TrainingSessionInscriptionId);
                if (validation != null)
                {
                    return BadRequest("Ya existe una firma.");
                }
                if (!trainingSessionInscriptionAttend.Signature.StartsWith("https://"))
                {
                    var trainingSession = Convert.FromBase64String(trainingSessionInscriptionAttend.Signature);
                    trainingSessionInscriptionAttend.Signature = await _fileStorage.SaveFileAsync(trainingSession, ".jpg", "signature");
                }
                _context.Add(trainingSessionInscriptionAttend);
                await _context.SaveChangesAsync();
                return Ok(trainingSessionInscriptionAttend);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe una firma.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var trainingSessionAttend = await _context.TrainingSessionInscriptionAttends
                .Include(i => i.TrainingSessionInscription!)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (trainingSessionAttend == null)
            {
                return NotFound();
            }
            return Ok(trainingSessionAttend);
        }
    }
}
