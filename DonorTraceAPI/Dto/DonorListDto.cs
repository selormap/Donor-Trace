using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceAPI.Models;

namespace DonorTraceAPI.Dto
{
    public class DonorListDto
    {
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Name { get; set; }
      
        public string Region { get; set; }
        public string BloodType { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public ICollection<OrganList> DonorOrgans { get; set; }
    }
}
