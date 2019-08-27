using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorTraceAPI.Data;
using DonorTraceAPI.Dto;
using DonorTraceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonorTraceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoController : ControllerBase
    {
        private readonly DataContext _db;

        public RepoController(DataContext db)
        {
            _db = db;
        }

        [HttpGet("bloodgroups")]
        public IEnumerable<BloodGroup> GetBloodGroups() => _db.BloodGroups;

        [HttpGet("bloodtype/{id}")]
        public IEnumerable<BloodType> GetBloodType(int id)
        {
            return _db.BloodTypes.Where(t => t.BloodGroup.Id == id);

        }

        [HttpGet("regions")]
        public IEnumerable<Region> Get() => _db.Regions;

        [HttpGet("organs")]
        public IEnumerable<OrganList> GetOrgans() => _db.OrganLists;

        [HttpPost("donate")]
        public async Task<IActionResult> Post(DonorDto donarUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new Donor
            {

            };
           


            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}