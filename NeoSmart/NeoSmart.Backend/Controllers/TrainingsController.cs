using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Intertfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrainingsController : GenericController<Training>
    {
        private readonly DataContext _context;

        public TrainingsController(IGenericUnitOfWork<Training> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
            _context = context;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Trainings
                                .Include(o => o.Occupation)
                                .Include(o => o.TrainingTopics)
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
            var queryable = _context.Trainings
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
            var state = await _context.Trainings
                .Include(o => o.Occupation)
                .Include(o => o.TrainingTopics)
                .ThenInclude(x => x.Topic)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(TrainingDTO trainingDTO)
        {
            try
            {
                Training newTraining = new()
                {
                    Cod = trainingDTO.Cod,
                    Description = trainingDTO.Description,
                    OccupationId = trainingDTO.OccupationId,
                    TrainingTopics = new List<TrainingTopic>()
                };

                foreach (var trainingTopicId in trainingDTO.TrainingTopicIds!)
                {
                    var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == trainingTopicId);
                    if (topic != null)
                    {
                        newTraining.TrainingTopics.Add(new TrainingTopic { Topic = topic });
                    }
                }

                _context.Add(newTraining);
                await _context.SaveChangesAsync();
                return Ok(trainingDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe un entrenamiento con el mismo nombre.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("full")]
        public async Task<ActionResult> PutFullAsync(TrainingDTO trainingDTO)
        {
            try
            {
                var training = await _context.Trainings
                    .Include(x => x.TrainingTopics)
                    .FirstOrDefaultAsync(x => x.Id == trainingDTO.Id);
                if (training == null)
                {
                    return NotFound();
                }

                training.Cod = trainingDTO.Cod;
                training.Description = trainingDTO.Description;
                training.OccupationId = trainingDTO.OccupationId;
                if (trainingDTO.TrainingTopicIds != null && trainingDTO.TrainingTopicIds.Count > 0)
                {
                    training.TrainingTopics = trainingDTO.TrainingTopicIds!.Select(x => new TrainingTopic { TopicId = x }).ToList();
                }

                _context.Update(training);
                await _context.SaveChangesAsync();
                return Ok(trainingDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe un entrenamiento con el mismo nombre.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
