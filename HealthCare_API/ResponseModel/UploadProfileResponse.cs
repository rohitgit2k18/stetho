using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
  public  class UploadProfileResponse
    {
        public UploadProfileResponseObject Response { get; set; }
    }
    public class UploadProfileResponseObject
    {
        public int StatusCode { get; set; }
        public string message { get; set; }
    }
}
