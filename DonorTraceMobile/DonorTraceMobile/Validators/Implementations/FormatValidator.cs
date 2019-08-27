﻿using DonorTraceMobile.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DonorTraceMobile.Validators.Implementations
{
    public class FormatValidator: IValidator
    {
        public string Message { get; set; } = "Invalid format";
        public string Format { get; set; }

        public bool Check(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex format = new Regex(Format);

                return format.IsMatch(value);
            }
            else
            {
                return false;
            }
        }
        public bool Compare(string value)
        {
            return false;
        }


    }
}
