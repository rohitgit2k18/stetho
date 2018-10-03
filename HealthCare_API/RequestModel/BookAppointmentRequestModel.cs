using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.RequestModel
{
  public  class BookAppointmentRequestModel
    {
        public int AppointmentId { get; set; }
        public string ReasonForVisit { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }      
        public string Illness { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public string ProfilePicture { get; set; }
        public string DocName { get; set; }
        public string Qualification { get; set; }
        public string dept { get; set; }
        public string hospitalname { get; set; }
    }
    public class BookAppointmentRequest
    {
 
         public int AppointmentId { get; set; }
        public string ReasonForVisit { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Illness { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int UserId { get; set; }
        //public string ProfilePicture { get; set; }
        //public string DocName { get; set; }
        //public string Qualification { get; set; }
        //public string dept { get; set; }
        //public string hospitalname { get; set; }
    }
}
