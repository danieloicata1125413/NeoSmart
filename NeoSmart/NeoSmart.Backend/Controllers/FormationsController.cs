﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;
using System.ComponentModel.Design;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class FormationsController : GenericController<Formation>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public FormationsController(IGenericUnitOfWork<Formation> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Formations
                                .Include(o => o.Company)
                                .Include(o => o.FormationTopics)
                                .Include(o => o.FormationOccupations)
                                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Company!.Id == user.Company!.Id);
            }
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(s => s.Company!.Name)
                .ThenBy(s => s.Description)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public override async Task<ActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Formations
                .AsQueryable();
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Company!.Id == user.Company!.Id);
            }
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Description.ToLower().Contains(pagination.Filter.ToLower()));
            }
            queryable = queryable
                .OrderBy(s => s.Company!.Name)
                .ThenBy(s => s.Description);
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var formation = await _context.Formations
                .Include(o => o.Company)
                .Include(o => o.FormationTopics!)
                .ThenInclude(x => x.Topic)
                .Include(o => o.FormationOccupations!)
                .ThenInclude(x => x.Occupation)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (formation == null)
            {
                return NotFound();
            }
            return Ok(formation);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(FormationDTO formationDTO)
        {
            try
            {
                Formation newFormation = new()
                {
                    CompanyId = formationDTO.CompanyId,
                    Description = formationDTO.Description,
                    FormationTopics = new List<FormationTopic>(),
                    FormationOccupations = new List<FormationOccupation>(),
                    Status = formationDTO.Status,
                };
                foreach (var FormationTopicId in formationDTO.FormationTopicIds!)
                {
                    var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == FormationTopicId);
                    if (topic != null)
                    {
                        newFormation.FormationTopics.Add(new FormationTopic { Topic = topic });
                    }
                }

                foreach (var FormationOccupationId in formationDTO.FormationOccupationIds!)
                {
                    var occupation = await _context.Occupations.FirstOrDefaultAsync(x => x.Id == FormationOccupationId);
                    if (occupation != null)
                    {
                        newFormation.FormationOccupations.Add(new FormationOccupation { Occupation = occupation });
                    }
                }

                _context.Add(newFormation);
                await _context.SaveChangesAsync();
                return Ok(formationDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe un entrenamiento con el mismo código.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("full")]
        public async Task<ActionResult> PutFullAsync(FormationDTO formationDTO)
        {
            try
            {
                var formation = await _context.Formations
                    .Include(x => x.FormationTopics)
                    .FirstOrDefaultAsync(x => x.Id == formationDTO.Id);
                if (formation == null)
                {
                    return NotFound();
                }
                formation.CompanyId = formationDTO.CompanyId;
                formation.Description = formationDTO.Description;
                if (formationDTO.FormationTopicIds != null && formationDTO.FormationTopicIds.Count > 0)
                {
                    formation.FormationTopics = formationDTO.FormationTopicIds!.Select(x => new FormationTopic { TopicId = x }).ToList();
                }
                if (formationDTO.FormationOccupationIds != null && formationDTO.FormationOccupationIds.Count > 0)
                {
                    formation.FormationOccupations = formationDTO.FormationOccupationIds!.Select(x => new FormationOccupation { OccupationId = x }).ToList();
                }
                formation.Status = formationDTO.Status;
                _context.Update(formation);
                await _context.SaveChangesAsync();
                return Ok(formationDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe un entrenamiento con el mismo código.");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
