using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.RequestModel
{
  public  class OTPRequestModel
    {
        public string PhoneNo { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
    }
}
