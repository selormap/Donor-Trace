using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceAPI.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        [Required, StringLength(150)]
        public string Organization { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, StringLength(150)]
        public string Location { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
