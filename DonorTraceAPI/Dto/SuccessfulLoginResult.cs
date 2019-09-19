using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorTraceAPI.Dto
{
    public class SuccessfulLoginResult
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
    }
}
