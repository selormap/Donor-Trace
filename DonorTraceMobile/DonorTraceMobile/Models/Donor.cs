using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceMobile.Models
{
    public class Donor
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }
        public string Location { get; set; }
        public int? Longitude { get; set; }
        public int? Latitude { get; set; }
        public int RegionId { get; set; }

        public string Phone { get; set; }
        public string ImagePath { get; set; }

        public object ImageArray { get; set; }

       
    }
}
