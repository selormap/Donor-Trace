using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bict.Hubtel.Base;
using DonorTraceAPI.Data;
using DonorTraceAPI.Dto;
using DonorTraceAPI.Helpers;
using DonorTraceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Remotion.Linq.Clauses;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DonorTraceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoController : ControllerBase
    {
        private readonly DataContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public RepoController(DataContext db, UserManager<User> userManager, IConfiguration config)
        {
            _db = db;
            _userManager = userManager;
            _config = config;
        }
        [HttpGet("donor-exist/{id}")]
        public async Task<IActionResult> DonorExist(string id)
        {
            var donor = await _db.Donors.FirstOrDefaultAsync(d => d.Id == id);

            if (donor == null)
            {
                return BadRequest("Donor Already exists");
            }
            return Ok(donor);
        }
    
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
            var profile = _db.Donors.Where(d => d.Id == id)
                .Select(n => new DonorProfileDto()
                {
                    Id = n.Id,
                    Name = n.FirstName + " " + n.LastName,
                    Email = n.Email,
                    Gender = ((Donor.Sex)n.Gender).ToString(),
                    Location = n.Location,
                    Region = n.Region.Name,
                    Phone = n.Phone,
                    ImagePath = n.ImagePath

                }).FirstOrDefault();

            //if (donor == null)

            //{

            //    return NotFound();

            //}
            return Ok(profile);
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
        public IEnumerable<BloodType> GetBloodTypes()
        {
            return _db.BloodTypes;

        }

        [HttpGet("regions")]
        public IEnumerable<Region> GetRegions() => _db.Regions;

        [HttpGet("organs")]
        public IEnumerable<OrganList> GetOrgans() => _db.OrganLists;

        [HttpGet("facilities")]
        public IEnumerable<Facility> GetFacilities() => _db.Facilities;

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
        public async Task<IActionResult> AddDonor(DonorDto donorUser)
        {
            // check if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //upload camera image to contents folder
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
            // save donor info to database
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
                return Ok(); //action successful
            }
            return BadRequest("Could not save donor information"); //failed 
        }

        [HttpPost("facility")]
        public async Task<IActionResult> AddFacility(FacilityDto model)
        {
            //check if model state is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Create facility object
            var facility = new Facility
            {
                Name = model.Name,
                RegistrationNo = model.RegistrationNo,
                Address = model.Address,
                ContactNo = model.ContactNo,
                CreatedBy = model.CreatedBy,
                Created = DateTime.Now
            };
            //save facility to database
            _db.Facilities.Add(facility);
            if (await _db.SaveChangesAsync() > 0)
                return Ok(); // save operation successful

            return BadRequest("Could not save facility"); // save operation failed
        }

        [HttpPost("officer")]
        public async Task<IActionResult> AddOfficer(OfficerDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _userManager.FindByEmailAsync(model.UserName);
            if (user != null) return BadRequest(ModelState);

            var now = DateTime.Now;

            user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                CreatedDate = now,
                LastModifiedDate = now

            };
            Random rnd = new Random();
            int num = rnd.Next(1, 9);
            string password = PinGenerator.RandomString(4) + "@" + num;
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "Medical Officer");
            if (result.Succeeded)
            {
                var medical = new MedicalOfficer()
                {
                    UserName = model.UserName,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    FacilityId = model.FacilityId,
                    Department = model.Department,
                    ContactNo = model.ContactNo,
                    CreatedBy = model.CreatedBy,
                    Created = DateTime.Now
                };

                _db.Officers.Add(medical);
                if (await _db.SaveChangesAsync() > 0)
                {
                    SendSms(model.UserName, password, "Donor Trace", model.ContactNo);
                    return Ok();

                }

            }


            return BadRequest("Could not save officer");
        }

        [HttpPost("sendsms")]
        public void Sms(SmsDto sms)
        {
            string clientId = _config.GetValue<string>("Sms:ClientId");
            string clientSecret = _config.GetValue<string>("Sms:ClientSecret");

            StringBuilder builder = new StringBuilder();
            builder.Append("You have been registered on the Donor Trace Mobile App");
           

            var host = new ApiHost(new BasicAuth(clientId, clientSecret));
            var messageApi = new MessagingApi(host);
            MessageResponse msg = messageApi.SendQuickMessage("Donor Trace", sms.To, builder.ToString(), true);
            //return Ok(msg);
        }
        public void SendSms(string username, string password , string from, string to)
        {
            string clientId = _config.GetValue<string>("Sms:ClientId");
            string clientSecret = _config.GetValue<string>("Sms:ClientSecret");

            StringBuilder builder = new StringBuilder();
            builder.Append("You have been registered on the Donor Trace Mobile App");
            builder.AppendLine();
            builder.Append("Username: " + " " + username);
            builder.AppendLine();
            builder.Append("Password: " + " " + password);

            var host = new ApiHost(new BasicAuth(clientId, clientSecret));
            var messageApi = new MessagingApi(host);
            MessageResponse msg = messageApi.SendQuickMessage(from, to, builder.ToString(), true);


        }
    }
}