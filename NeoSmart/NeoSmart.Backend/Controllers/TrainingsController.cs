using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.BackEnd.Interfaces;
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
        private readonly IUserHelper _userHelper;

        public TrainingsController(IGenericUnitOfWork<Training> unitOfWork, DataContext context, IFileStorage fileStorage, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
            _userHelper = userHelper;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Trainings
                                .Include(o => o.Process!)
                                    .ThenInclude(o => o.Company)
                                .Include(o => o.TrainingTopics!)
                                    .ThenInclude(x => x.Topic!)
                                .Include(o => o.TrainingImages!)
                                .Include(o => o.TrainingSessions!)
                                    .ThenInclude(o => o.User)
                                    .ThenInclude(o => o.City!)
                                .Include(o => o.TrainingStatus!)
                                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Process!.Company!.Id == user.Company!.Id);
            }
            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.TrainingStatusId == pagination.Id);
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
            var queryable = _context.Trainings
                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Process!.Company!.Id == user.Company!.Id);
            }
            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.TrainingStatusId == pagination.Id);
            }
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
            var training = await _context.Trainings
                                .Include(o => o.Process!)
                                .ThenInclude(o => o.Company)
                                .Include(o => o.TrainingTopics!)
                                .ThenInclude(x => x.Topic)
                                .Include(o => o.TrainingImages)
                                .Include(o => o.TrainingSessions!)
                                .ThenInclude(o => o.User)
                                .ThenInclude(o => o.City!)
                                .Include(o => o.TrainingStatus!)
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
                    ProcessId = trainingDTO.ProcessId,
                    Cod = trainingDTO.Cod.ToUpper(),
                    Description = trainingDTO.Description,
                    Requirement = trainingDTO.Requirement,
                    DateStart = trainingDTO.DateStart,
                    Duration = trainingDTO.Duration,
                    Type = trainingDTO.Type,
                    Entity = trainingDTO.Entity,
                    Price = trainingDTO.Price,
                    TrainingStatusId = trainingDTO.TrainingStatusId,
                    TrainingTopics = new List<TrainingTopic>(),
                    TrainingImages = new List<TrainingImage>(),
                    Status = trainingDTO.Status,
                };

                foreach (var trainingImage in trainingDTO.NewTrainingImages!)
                {
                    var photoTraining = Convert.FromBase64String(trainingImage);
                    newTraining.TrainingImages.Add(new TrainingImage { Image = await _fileStorage.SaveFileAsync(photoTraining, ".jpg", "trainings") });
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
                    .Include(o => o.TrainingStatus!)
                    .FirstOrDefaultAsync(x => x.Id == trainingDTO.Id);
                if (training == null)
                {
                    return NotFound();
                }
                training.ProcessId = trainingDTO.ProcessId;
                training.Cod = trainingDTO.Cod.ToUpper();
                training.Description = trainingDTO.Description;
                training.Requirement = trainingDTO.Requirement;
                training.DateStart = trainingDTO.DateStart;
                training.Duration = trainingDTO.Duration;
                training.Type = trainingDTO.Type;
                training.Entity = trainingDTO.Entity;
                training.Price = trainingDTO.Price;
                training.TrainingStatusId = trainingDTO.TrainingStatusId;

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

        [HttpPost("addImages")]
        public async Task<ActionResult> PostAddImagesAsync(ImageDTO imageDTO)
        {
            var training = await _context.Trainings
                .Include(x => x.TrainingImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.Id);
            if (training == null)
            {
                return NotFound();
            }

            for (int i = 0; i < imageDTO.Images.Count; i++)
            {
                if (!imageDTO.Images[i].StartsWith("https://"))
                {
                    var photoTraining = Convert.FromBase64String(imageDTO.Images[i]);
                    imageDTO.Images[i] = await _fileStorage.SaveFileAsync(photoTraining, ".jpg", "trainings");
                    training.TrainingImages!.Add(new TrainingImage { Image = imageDTO.Images[i] });
                }
            }

            _context.Update(training);
            await _context.SaveChangesAsync();
            return Ok(training!.TrainingImages.ToList());
        }

        [HttpPost("removeImages")]
        public async Task<ActionResult> PostDeleteImagesAsync(ImageDTO imageDTO)
        {
            var trainingImage = await _context.TrainingImages
                .FirstOrDefaultAsync(x => x.Id == imageDTO.Id);
            if (trainingImage == null)
            {
                return NotFound();
            }

            _context.Remove(trainingImage);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("removeLastImage")]
        public async Task<ActionResult> PostRemoveLastImageAsync(ImageDTO imageDTO)
        {
            var training = await _context.Trainings
                .Include(x => x.TrainingImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.Id);
            if (training == null)
            {
                return NotFound();
            }

            if (training.TrainingImages is null || training.TrainingImages.Count == 0)
            {
                return Ok();
            }

            var lastImage = training.TrainingImages.LastOrDefault();
            await _fileStorage.RemoveFileAsync(lastImage!.Image, "trainings");
            training.TrainingImages.Remove(lastImage);
            _context.Update(training);
            await _context.SaveChangesAsync();
            imageDTO.Images = training.TrainingImages.Select(x => x.Image).ToList();
            return Ok(imageDTO);
        }
    }
}
