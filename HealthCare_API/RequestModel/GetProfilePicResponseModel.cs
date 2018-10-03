using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.RequestModel
{
   public class GetProfilePicResponseModel
    {
        public GetProfilePicResponseModelObject Response { get; set; }
    }
    public class GetProfilePicResponseModelObject
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ProfilePicture { get; set; }
    }
}
