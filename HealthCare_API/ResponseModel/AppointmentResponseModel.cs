using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
   public class AppointmentResponseModel
    {
        public ApiResponse Response { get; set; }
    }

    public class AppointmentList
    {
        public AppointmentList(int appointmentId, string doctorName, string previousDate, string nextDate, string doctorProfilePic, int queueNumber, string appointmentDate, string appointmentTime, string qualificationName,string reasonForVisit, string hospitalName )
        {
            AppointmentId = appointmentId;
            DoctorName = doctorName;
            PreviousDate = previousDate;
            NextDate = nextDate;
            DoctorProfilePic = doctorProfilePic;
            QueueNumber = queueNumber;
            AppointmentDate = appointmentDate;
            AppointmentTime = appointmentTime;
            QualificationName = qualificationName;
            ReasonForVisit = reasonForVisit;
            HospitalName = hospitalName;
        }
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public string PreviousDate { get; set; }
        public string NextDate { get; set; }
        public string DoctorProfilePic { get; set; }
        public int QueueNumber { get; set; }      
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string QualificationName { get; set; }
        public string ReasonForVisit { get; set; }
        public string HospitalName { get; set; }


      
    }

    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<AppointmentList> AppointmentList { get; set; }
    }

    
}
