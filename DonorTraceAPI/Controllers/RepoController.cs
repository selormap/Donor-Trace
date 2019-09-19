using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DonorTraceAPI.Data;
using DonorTraceAPI.Dto;
using DonorTraceAPI.Helpers;
using DonorTraceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace DonorTraceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly UserManager<User> _userManager;

        public RepoController(DataContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("donors")]
        public async Task<IEnumerable<DonorListDto>> GetAllDonors()

        {
            var donors = await (_db.Donors.Select(d => new DonorListDto
            {
                Id = d.Id,
                Gender = d.Gender.ToString(),
                Name = d.FirstName + " " + d.LastName,
                Region = _db.Regions.FirstOrDefault(r => r.Id == d.RegionId).Name,
                BloodType =
                    (_db.BloodTypes.Join(_db.DonorOrgans, l => l.Id, g => g.BloodTypeId, (l, g) => new {l, g})
                        .Where(@t => d.Id == @t.g.UserId)
                        .Select(@t => @t.l.Name)).FirstOrDefault() ?? "None",
                DonorOrgans = (from r in _db.OrganLists
                    join o in _db.DonorOrgans on r.Id equals o.OrganListId
                    where d.Id == o.UserId
                    select r).Distinct().ToList(),
                ImagePath = d.ImagePath,
            })).ToListAsync();

         

            return donors;

        }

        [HttpGet("officer/{id}")]
        public async Task<IActionResult> GetOfficer(string id)
        {
            var userName = await _userManager.FindByIdAsync(id);
            var officer = _db.Officers.Where(o => o.UserName == userName.UserName)
                .Select(n => new OfficerDto()
                {
                    Name = n.Firstname + " " + n.Lastname,
                    Facility = n.Facility.Name
                }).FirstOrDefault();

            return Ok(officer);
        }

        [HttpGet("donors/{id}")]
        public async Task<IActionResult> GetDonor(string id)
        {
            Donor donor = await _db.Donors.FindAsync(id);

            if (donor == null)

            {

                return NotFound();

            }
            return Ok(donor);
        }

        [HttpGet("blood-type/{id}")]
        public async Task<IActionResult> GetBloodType(string id)
        {
            var donor = await (from d in _db.DonorOrgans
                    join b in _db.BloodTypes
                        on d.BloodTypeId equals b.Id
                    where d.UserId == id
                    select new BloodOptionDto
                    {
                        BloodType = _db.BloodTypes.FirstOrDefault(a => a.Id == d.BloodTypeId).Name
                    }
                ).FirstOrDefaultAsync();

            if (donor == null)

            {
                return Ok(new BloodOptionDto
                {
                    BloodType = "None"
                });
            }

            return Ok(donor);
        }

        [HttpGet("organ/{id}")]
       
        public async Task<IActionResult> GetOrganType(string id)
        {
            var donor = await (from d in _db.DonorOrgans
                    join b in _db.OrganLists
                        on d.OrganListId equals b.Id
                    where d.UserId == id && d.OrganListId != 0
                               select new {b.Name
                    
                }).ToListAsync();

            if (donor == null)

            {

                return NotFound();

            }

            return Ok(donor);
        }

        [HttpGet("bloodgroups")]
        public IEnumerable<BloodGroup> GetBloodGroups() => _db.BloodGroups;

        [HttpGet("bloodtype/{id}")]
        public IEnumerable<BloodType> GetBloodType(int id)
        {
            return _db.BloodTypes.Where(t => t.BloodGroup.Id == id);

        }
        [HttpGet("blood-type")]
        public IEnumerable<BloodType> GetBloodType()
        {
            return _db.BloodTypes;

        }

        [HttpGet("regions")]
        public IEnumerable<Region> Get() => _db.Regions;

        [HttpGet("organs")]
        public IEnumerable<OrganList> GetOrgans() => _db.OrganLists;

        [HttpPost("organ-option")]
        public async Task<IActionResult> Post(OrganOptionDto organOption)
        {
           

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var option = new DonorOrgan
            {
                UserId = organOption.UserId,
                BloodTypeId = (int?)organOption.BloodTypeId,
               OrganListId = (int?)organOption.OrganListId
            };

            _db.DonorOrgans.Add(option);
            if(await _db.SaveChangesAsync() > 0)
                return Ok();

            return BadRequest("Could not save donation option");
        }

        [HttpPost("donate")]
        public async Task<IActionResult> Post(DonorDto donorUser)
        {

          

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stream = new MemoryStream(donorUser.ImageArray);

            var guid = Guid.NewGuid().ToString();

            var file = $"{guid}.jpg";

            var folder = "~/Contents/Donors";

            var fullPath = $"{folder}/{file}";

            var response = FilesHelper.UploadPhoto(stream, file);

            if (response)

            {

                donorUser.ImagePath = fullPath;

            }

            DateTime now = DateTime.Now;
            var user = new Donor
            {
                Id = donorUser.Id,
                FirstName = donorUser.FirstName,
                LastName = donorUser.LastName,
                Email = donorUser.Email,
                Phone = donorUser.Phone,
                Gender = donorUser.Gender,
                RegionId = donorUser.RegionId,
                Location = donorUser.Location,
                Longitude = donorUser.Longitude,
                Latitude = donorUser.Latitude,
                ImagePath = donorUser.ImagePath,
                CreatedDate = now,
                LastUpdateDate = now
            };

            _db.Donors.Add(user);
            if (await _db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            return BadRequest("Could not save donor information");
        }
    }
}