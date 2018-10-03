using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
   public class RegisterUserResponse
    {
        public RegResponse Response { get; set; }
    }
    public class RegResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
