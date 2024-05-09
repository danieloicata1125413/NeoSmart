﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CompaniesController : GenericController<Company>
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public CompaniesController(IGenericUnitOfWork<Company> unitOfWork, DataContext context, IUserHelper userHelper) : base(unitOfWork, context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet("comboAll")]
        public async Task<ActionResult> GetComboAllAsync()
        {
            return Ok(await _context.Companies
                .OrderBy(c => c.Name)
                .ToListAsync());
        }

        [HttpGet("combo")]
        public async Task<ActionResult> GetComboAsync()
        {
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company == null)
            {
                return Ok(await _context.Companies
                .OrderBy(c => c.Name)
                .ToListAsync());
            }
            return Ok(await _context.Companies
                .Where(c => c.Id == user.Company.Id)
                .OrderBy(c => c.Name)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Companies
                                .Include(t => t.City)
                                .Include(t => t.Users)
                                .Include(t => t.Process)
                                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Id == user.Company!.Id);
            }
            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var company = await _context.Companies
                .Include(i => i.City!)
                     .ThenInclude(i => i.State!)
                     .ThenInclude(i => i.Country!)
                .Include(t => t.Users)
                .Include(t => t.Process)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
    }
}
