using System;
using System.Collections.Generic;
using System.Text;

namespace DonorTraceMobile.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
