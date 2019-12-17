using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceAPI.Dto
{
    public class FacilityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RegistrationNo { get; set; }

        public string Address { get; set; }
 
        public string ContactNo { get; set; }
  
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
