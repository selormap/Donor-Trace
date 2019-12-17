using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorTraceMobile.Models
{
    public class DonorModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BloodType { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
        public string Gender  { get; set; }
        public string ImagePath { get; set; }
        public string Phone { get; set; }
        public ICollection<DonorOrgan> DonorOrgans { get; set; }

        public string FullLogoPath
        {
            get
            {


               // return String.Format("https://dtrace.azurewebsites.net/{0}", ImagePath.Substring(2)); 
                return String.Format("http://10.0.2.2:5000/{0}", ImagePath.Substring(2));
            }
        }
    }
}
