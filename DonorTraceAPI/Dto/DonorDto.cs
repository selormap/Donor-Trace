using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceAPI.Models;

namespace DonorTraceAPI.Dto
{
    public class DonorDto
    {
        public string Id { get; set; }
       
        public string Email { get; set; }
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

      
        public Donor.Sex Gender { get; set; }
        public string Location { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int RegionId { get; set; }
    
        public string Phone { get; set; }
        public string ImagePath { get; set; }
    
        public byte[] ImageArray { get; set; }

       
    }
}
