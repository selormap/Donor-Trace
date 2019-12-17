using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceMobile.Models
{
    public class OfficerModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Facility { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string ContactNo { get; set; }

        public int FacilityId { get; set; }

        public string Department { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
