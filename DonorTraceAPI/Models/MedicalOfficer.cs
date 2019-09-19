using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceAPI.Models
{
    public class MedicalOfficer
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string Firstname { get; set; }
        [Required, MaxLength(50)]
        public string Lastname { get; set; }
        [Required, MaxLength(10)]
        public string ContactNo { get; set; }

        public int FacilityId { get; set; }

        public string Department { get; set; }
        [Required, StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }

        public Facility Facility { get; set; }
    }
}
