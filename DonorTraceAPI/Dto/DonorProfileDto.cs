using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonorTraceAPI.Models;

namespace DonorTraceAPI.Dto
{
    public class DonorProfileDto
    {
        public string Id { get; set; }

        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }
        public string Location { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Region { get; set; }
        public int RegionId { get; set; }

        public string Phone { get; set; }
        public string ImagePath { get; set; }

        public byte[] ImageArray { get; set; }
    }
}
