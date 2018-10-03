using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
    public class OTPResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; } 
        public int OTP { get; set; }     
       
    }

    public class OTPResponseModel
    {
        public OTPResponse Response { get; set; }
    }
 
}
