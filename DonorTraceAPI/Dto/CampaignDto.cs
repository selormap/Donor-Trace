using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceAPI.Dto
{
    public class CampaignDto
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
