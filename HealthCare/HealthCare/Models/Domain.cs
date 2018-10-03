using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Models
{
  public static class Domain
    {
        public static string Url
        {
          
            get
            {
                 return "http://18.219.253.188:8081/";
               
            }
        }

        public static string LoginApiConstant
        {
            get
            {
                return "api/User/Login";
            }
        }
        public static string OTPVerificationApiConstant
        {
            get
            {
                return "api/User/ConfirmOtp";
            }
        }
        public static string UpdatePINApiConstant
        {
            get
            {
                return "api/User/EditProfile";
            }
        }
        public static string AppointmentApiConstant
        {
            get
            {
                return "api/Appoinment/AppoinmentList";
            }
        }

        public static string BookAppoinmentApiConstant
        {
            get
            {
                return "api/Appoinment/BookAppoinment";
            }
        }
        public static string DoctorDetailApiConstant
        {
            get
            {
                return "api/Patient/GetDoctorListByDepartment";
            }
        }
        public static string DepartmentApiConstant
        {
            get
            {
                return "api/Patient/GetDeparment";
            }
        }
        public static string PrescriptionApiConstant
        {
            get
            {
                return "api/Patient/PrescriptionList";
            }
        }
        public static string ReportApiConstant
        {
            get
            {
                return "api/Patient/ReportListByUserIdUpdated";
            }
        }
        public static string LOgOutApiConstant
        {
            get
            {
                return "api/User/Logout";
            }
        }
        public static string GetReasonForVisitApiConstant
        {
            get
            {
                return "api/Patient/GetReasonForVisit";
            }
        }
        public static string RegisterUserApiConstant
        {
            get
            {
                return "api/User/SignUp";
            }
        }
        public static string EditUserApiConstant
        {
            get
            {
                return "api/User/EditUser";
            }
        }
        public static string UpdateUserApiConstant
        {
            get
            {
                return "api/User/UpdateUser/";
            }
        }
        public static string UpdateUserProfileApiConstant
        {
            get
            {
                return "api/User/UploadUserProfile";
            }
        }
        public static string GetUserProfileApiConstant
        {
            get
            {
                return "api/User/GetUserImage";
            }
        }
    }
}
