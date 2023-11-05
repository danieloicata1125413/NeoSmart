using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.BackEnd.Intertfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;
using System.Diagnostics;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrainingsController : GenericController<Training>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public TrainingsController(IGenericUnitOfWork<Training> unitOfWork, DataContext context, IFileStorage fileStorage) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Trainings
                                .Include(o => o.Process!)
                                .Include(o => o.TrainingTopics!)
                                .ThenInclude(x => x.Topic)
                                .Include(o => o.TrainingImages)
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var training = await _context.Trainings
                                .Include(o => o.Process!)
                                .Include(o => o.TrainingTopics!)
                                .ThenInclude(x => x.Topic)
                                .Include(o => o.TrainingImages)
                                .FirstOrDefaultAsync(s => s.Id == id);
            if (training == null)
            {
                return NotFound();
            }
            return Ok(training);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(TrainingDTO trainingDTO)
        {
            try
            {
                Training newTraining = new()
                {
                    Cod = trainingDTO.Cod.ToUpper(),
                    Description = trainingDTO.Description,
                    Type = trainingDTO.Type,
                    Duration = trainingDTO.Duration,
                    ProcessId = trainingDTO.ProcessId,
                    TrainingTopics = new List<TrainingTopic>(),
                    TrainingImages = new List<TrainingImage>(),
                    Status = trainingDTO.Status,
                };
                foreach (var trainingImage in trainingDTO.NewTrainingImages!)
                {
                    var photoTraining = Convert.FromBase64String(trainingImage);
                    newTraining.TrainingImages.Add(new TrainingImage { Image = await _fileStorage.SaveFileAsync(photoTraining, ".jpg", "Trainings") });
                }

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
                return BadRequest("Ya existe un capacitación con el mismo codigo.");
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
                    .Include(x => x.TrainingTopics!)
                    .ThenInclude(x => x.Topic)
                    .Include(x => x.Process)
                    .FirstOrDefaultAsync(x => x.Id == trainingDTO.Id);
                if (training == null)
                {
                    return NotFound();
                }

                training.Cod = trainingDTO.Cod.ToUpper();
                training.Description = trainingDTO.Description;
                training.Type = trainingDTO.Type;
                training.Duration = trainingDTO.Duration;
                training.ProcessId = trainingDTO.ProcessId;
                if (trainingDTO.TrainingTopicIds != null && trainingDTO.TrainingTopicIds.Count > 0)
                {
                    training.TrainingTopics = trainingDTO.TrainingTopicIds!.Select(x => new TrainingTopic { TopicId = x }).ToList();
                }
                training.Status = trainingDTO.Status;
                _context.Update(training);
                await _context.SaveChangesAsync();
                return Ok(trainingDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe un capacitación con el mismo codigo.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
