using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceAPI.Models
{
    public class Donor
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public Sex Gender { get; set; }
        public string Location { get; set; }
        public int? Longitude { get; set; }
        public int? Latitude { get; set; }
        public int RegionId { get; set; }
        [Required, MaxLength(10)]
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public byte[] ImageArray { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public Region Region { get; set; }


        public enum Sex
        {
            Female,
            Male
        }


    }
}
