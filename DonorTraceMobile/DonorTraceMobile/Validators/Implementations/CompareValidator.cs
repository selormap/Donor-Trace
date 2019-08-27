using DonorTraceMobile.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DonorTraceMobile.Validators.Implementations
{
    public class CompareValidator : IValidator
    {
      
        public string Message { get; set; } = "Passwords do not match";
        public string _password { get; set; }
        public string _confirmPassword { get; set; }
       

        public bool Check(string value)
        {
            return _password == value;
        }

       
    }
}
