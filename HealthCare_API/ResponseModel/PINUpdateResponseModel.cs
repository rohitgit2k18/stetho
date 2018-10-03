using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
   public class PINUpdateResponseModel
    {
        public PINUpdateResponse Response { get; set; }
    }
    public class PINUpdateResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
