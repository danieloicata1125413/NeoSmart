using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.ClassLibraries.Responses;
using NeoSmart.Data.Entities;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RequestsController : GenericController<Request>
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IUserHelper _userHelper;
        private readonly IMapper _mapper;
        private readonly IMailHelper _mailHelper;

        public RequestsController(IGenericUnitOfWork<Request> unitOfWork, DataContext context, IFileStorage fileStorage, IUserHelper userHelper, IMapper mapper, IMailHelper mailHelper) : base(unitOfWork, context)
        {
            _context = context;
            _fileStorage = fileStorage;
            _userHelper = userHelper;
            _mapper = mapper;
            _mailHelper = mailHelper;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Requests
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.RequestStatus!)
                                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.UserLeader!.Occupation!.Process!.Company!.Id == user.Company!.Id);
                queryable = queryable.Where(c => c.UserManager!.Occupation!.Process!.Company!.Id == user.Company!.Id);
            }
            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.RequestStatusId == pagination.Id);
            }
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(s => s.DateStart!)
                .ThenBy(s => s.RequestStatus!.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Requests
                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.UserLeader!.Occupation!.Process!.Company!.Id == user.Company!.Id);
                queryable = queryable.Where(c => c.UserManager!.Occupation!.Process!.Company!.Id == user.Company!.Id);
            }
            if (pagination.Id > 0)
            {
                queryable = queryable.Where(x => x.RequestStatusId == pagination.Id);
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
            var Request = await _context.Requests
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.RequestStatus!)
                                .FirstOrDefaultAsync(s => s.Id == id);
            if (Request == null)
            {
                return NotFound();
            }
            return Ok(Request);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(RequestDTO RequestDTO)
        {
            try
            {
                Request newRequest = _mapper.Map<Request>(RequestDTO);
                var userLider = await _userHelper.GetUserAsync(User.Identity!.Name!);
                if (userLider == null)
                {
                    return NotFound();
                }

                newRequest.UserLeaderId = userLider!.Id;
                _context.Add(newRequest);
                await _context.SaveChangesAsync();
                var userAdminList = await _userHelper.GetUserByRoleAsync("Admin"); //buscar el admin correspondiente
                if (userAdminList != null)
                {
                    foreach (var userAdmin in userAdminList.Where(x => x.CompanyId == userLider.CompanyId))
                    {
                        var response = SendRequestConfirmationEmailAsync(userAdmin, userLider, newRequest);
                    }

                }
                return Ok(RequestDTO);
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
        public async Task<ActionResult> PutFullAsync(RequestDTO RequestDTO)
        {
            try
            {
                var Request = await _context.Requests
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.RequestStatus!)
                                .FirstOrDefaultAsync(s => s.Id == RequestDTO.Id);
                if (Request == null)
                {
                    return NotFound();
                }

                Request.UserLeaderId = RequestDTO.UserLeaderId;
                Request.Description = RequestDTO.Description;
                Request.Requirement = RequestDTO.Requirement;
                Request.DateStart = RequestDTO.DateStart;
                Request.Duration = RequestDTO.Duration;
                Request.Type = RequestDTO.Type;
                Request.Entity = RequestDTO.Entity;
                Request.Price = RequestDTO.Price;
                Request.RequestStatusId = RequestDTO.RequestStatusId;
                Request.UserManagerId = RequestDTO.UserManagerId;
                _context.Update(Request);
                await _context.SaveChangesAsync();

                return Ok(RequestDTO);
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

        [HttpPut("Authorized")]
        public async Task<ActionResult> PutAuthorizedAsync(RequestDTO RequestDTO)
        {
            try
            {
                var Request = await _context.Requests
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserLeader!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.City!)
                                .Include(o => o.UserManager!)
                                    .ThenInclude(ts => ts.Occupation!)
                                    .ThenInclude(ts => ts.Process!)
                                .Include(o => o.RequestStatus!)
                                .FirstOrDefaultAsync(s => s.Id == RequestDTO.Id);
                if (Request == null)
                {
                    return NotFound();
                }
                Request.RequestStatusId = RequestDTO.RequestStatusId;
                Request.Observation = RequestDTO.Observation;
                var userLider = await _userHelper.GetUserAsync(Guid.Parse(RequestDTO.UserLeaderId!.ToString()));
                if (userLider == null)
                {
                    return NotFound();
                }
                var userAdmin = await _userHelper.GetUserAsync(User.Identity!.Name!);
                if (userAdmin == null)
                {
                    return NotFound();
                }
                Request.UserManagerId = userAdmin!.Id;
                _context.Update(Request);
                await _context.SaveChangesAsync();


                var userManagerList = await _userHelper.GetUserByRoleAsync("Manager"); //buscar el Manager correspondiente
                if (userManagerList != null)
                {
                    foreach (var userManager in userManagerList.Where(x => x.CompanyId == userLider!.CompanyId))
                    {
                        if (Request.RequestStatusId == 2)
                            SendRequestAuthorizedEmailAsync(userAdmin, userManager!, Request);
                        else
                            SendRequestNoAuthorizedEmailAsync(userAdmin, userLider!, Request);
                    }
                }

                return Ok(RequestDTO);
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

        private Response<string> SendRequestConfirmationEmailAsync(User userAdmin, User userLider, Request newRequest)
        {
            return _mailHelper.SendMail(userAdmin.FullName, userAdmin.Email!,
                $"NeoSmart - Nueva solicitud de capacitación.",
                $"<h1>NeoSmart - se ha generado una nueva solicitud de solicitud de capacitación</h1>" +
                $"<p>de {userLider.FullName} - solicitud:<p/>" +
                $"<p>de {newRequest.Description}<p/>" +
                $"<p>{userAdmin.FullName}, debes ingresar al panel de control para aprobar o rechazar la solicitud.</p>" +
                $"<p>Atentamente:</p>" +
                $"<p>Equipo de neosmart.</p>");
        }

        private Response<string> SendRequestAuthorizedEmailAsync(User userAdmin, User userManager, Request newRequest)
        {
            return _mailHelper.SendMail(userManager.FullName, userManager.Email!,
                $"NeoSmart - Autorización de solicitud de capacitación.",
                $"<h1>NeoSmart - se ha autorizado la solicitud de solicitud de capacitación</h1>" +
                $"<p>por {userAdmin.FullName} - solicitud:<p/>" +
                $"<p>de {newRequest.Description}<p/>" +
                $"<p>{userManager.FullName}, debes ingresar al panel de control para crear una capacitación para la solicitud.</p>" +
                $"<p>Atentamente:</p>" +
                $"<p>Equipo de neosmart.</p>");
        }

        private Response<string> SendRequestNoAuthorizedEmailAsync(User userAdmin, User userLider, Request newRequest)
        {
            return _mailHelper.SendMail(userLider.FullName, userLider.Email!,
                $"NeoSmart - No se autorizo la solicitud de capacitación.",
                $"<h1>NeoSmart - se ha rechazado la solicitud de solicitud de capacitación</h1>" +
                $"<p>por {userAdmin.FullName} - solicitud:<p/>" +
                $"<p>de {newRequest.Description}<p/>" +
                $"<p>Razón:<p/>" +
                $"<p>{newRequest.Observation}<p/>" +
                $"<p>Atentamente:</p>" +
                $"<p>Equipo de neosmart.</p>");
        }
    }
}
