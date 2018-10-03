using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
   public class OTPVerificationResponseModel
    {
        public OTPVerificationResponse Response { get; set; }
    }
    public class OTPVerificationResponse
    {
        //public int StatusCode { get; set; }
        //public string Message { get; set; }    
        //public string ProfilePicture { get; set; }
        //public int UserPin { get; set; }
        //public string Address { get; set; }
        //public string TokenCode { get; set; }
        //public int Id { get; set; }
        //public int RoleId { get; set; }        
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Gender { get; set; }
        //public string PhoneNo { get; set; }
        //public string DOB { get; set; }
        [System.Diagnostics.DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? DOB { get; set; }
        public string ProfilePicture { get; set; }
        public int UserPin { get; set; }
        public string Address { get; set; }
        public string TokenCode { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }
    }
}
