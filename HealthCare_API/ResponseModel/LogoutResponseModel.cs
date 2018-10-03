using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
   public class LogoutResponseModel
    {
        public LogOutResponse Response { get; set; }
    }
    public class LogOutResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    
}
