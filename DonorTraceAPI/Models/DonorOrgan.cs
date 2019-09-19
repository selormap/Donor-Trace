using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceAPI.Models
{
    public class DonorOrgan
    {
        public int Id { get; set; }
        [Required, StringLength(450)]
        public string UserId { get; set; }
        public int? BloodTypeId { get; set; }
        public int? OrganListId { get; set; }

       // public BloodType BloodType { get; set; }
      //  public OrganList OrganList { get; set; }

    }
}
