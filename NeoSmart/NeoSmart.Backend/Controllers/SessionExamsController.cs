using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Helpers;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SessionExamsController : GenericController<SessionExam>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IMapper _mapper;

        public SessionExamsController(IGenericUnitOfWork<SessionExam> unitOfWork, DataContext context, IUserHelper userHelper, IMailHelper mailHelper, IMapper mapper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var Exam = await _context.SessionExams
                .Include(i => i.Session!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingImages!)
                .Include(i => i.Session!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingTopics!)
                    .ThenInclude(i => i.Topic!)
                .Include(i => i.Session!)
                  .ThenInclude(i => i.User!)
                .Include(i => i.Session!)
                .Include(i => i.TrainingExam!)
                .ThenInclude(i => i.TrainingExamQuestions!)
                .Include(i => i.TrainingExam!)
                .ThenInclude(i => i.Training!)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (Exam == null)
            {
                return NotFound();
            }
            return Ok(Exam);
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyGetAsync([FromQuery] PaginationDTO pagination)
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var queryable = _context.SessionExams
                .Include(i => i.Session!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingImages!)
                .Include(i => i.Session!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingTopics!)
                    .ThenInclude(i => i.Topic!)
                .Include(i => i.Session!)
                  .ThenInclude(i => i.User!)
                .Include(i => i.Session!)
                .Include(i => i.TrainingExam!)
                    .ThenInclude(i => i.TrainingExamQuestions!)
                .Include(i => i.TrainingExam!)
                    .ThenInclude(i => i.Training!)
                .AsQueryable();

            //var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            //if (!isAdmin)
            //{
            //    queryable = queryable.Where(s => s.Session!.User!.Email == User.Identity!.Name);
            //}

            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.SessionId == pagination.Id);
            }

            return Ok(await queryable
                .OrderByDescending(x => x.DateEnd)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("mytotalPages")]
        public async Task<IActionResult> MyGetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var queryable = _context.SessionExams.AsQueryable();

            //var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            //if (!isAdmin)
            //{
            //    queryable = queryable.Where(s => s.Session!.User!.Email == User.Identity!.Name);
            //}

            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.SessionId == pagination.Id);
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.SessionExams
                .Where(s => s.SessionId == pagination.Id)
                .Include(i => i.Session!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingImages!)
                .Include(i => i.Session!)
                    .ThenInclude(i => i.Training!)
                    .ThenInclude(i => i.TrainingTopics!)
                    .ThenInclude(i => i.Topic!)
                .Include(i => i.Session!)
                  .ThenInclude(i => i.User!)
                .Include(i => i.Session!)
                .Include(i => i.TrainingExam!)
                    .ThenInclude(i => i.TrainingExamQuestions!)
                .Include(i => i.TrainingExam!)
                    .ThenInclude(i => i.Training!)
                .AsQueryable();

            return Ok(await queryable
                .OrderByDescending(x => x.DateEnd)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.SessionExams
                   .Where(s => s.SessionId == pagination.Id)
                   .AsQueryable();

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutFullAsync(SessionExamDTO model)
        {
            try
            {
                SessionExam sessionExam = _mapper.Map<SessionExam>(model);
                _context.Update(sessionExam);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateException)
            {
                return BadRequest("No se pudo crear la medición para el la sesión.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(SessionExamDTO model)
        {
            try
            {
                SessionExam sessionExam = _mapper.Map<SessionExam>(model);
                _context.Add(sessionExam);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateException)
            {
                return BadRequest("No se pudo crear la medición para el la sesión.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
