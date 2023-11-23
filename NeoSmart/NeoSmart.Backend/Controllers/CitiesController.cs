using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.Data.Entities;
using System;

namespace NeoSmart.BackEnd.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        private readonly DataContext _context;

        public CitiesController(IGenericUnitOfWork<City> unitOfWork, DataContext context) : base(unitOfWork, context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("combo/{stateId:int}")]
        public async Task<ActionResult> GetComboAsync(int stateId)
        {
            return Ok(await _context.Cities
                .Where(c => c.StateId == stateId)
                .OrderBy(c => c.Name)
                .ToListAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }




        //[HttpPost("totalPages2")]
        //public async Task<ActionResult> PostPages2Async(CityPaginationDTO pagination)
        //{
        //    var queryable = _context.Cities
        //        .Where(x => x.State!.Id == pagination.Id)
        //        .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(pagination.Filter))
        //    {
        //        queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        //    }
        //    if (pagination.predicate != null)
        //    {
        //        queryable = queryable.Where(pagination.predicate);
        //    }

        //    double count = await queryable.CountAsync();
        //    double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
        //    return Ok(totalPages);
        //}
    }
}
