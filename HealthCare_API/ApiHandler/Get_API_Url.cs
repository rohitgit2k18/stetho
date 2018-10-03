using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ApiHandler
{
    public class Get_API_Url
    {
        // for Login
        public string LoginApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        // for register
        public string OTPApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        public string UpdatePinApi(string BaseUsrl)
        {

            return BaseUsrl;


        }
        public string AppointmentListApi(string BaseUsrl)
        {

            return BaseUsrl;


        }
        public string BookAppointmentListApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        public string DoctorDetailListApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        public string DepartmentListApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        public string PrescriptionListApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        public string ReportListApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        public string LogOutApi(string BaseUsrl)
        {
            return BaseUsrl;
        }
        public string ReasonForVisitApi(string BaseUrl)
        {
            return BaseUrl;
        }
        public string RegisterUserApi(string BaseUrl)
        {
            return BaseUrl;
        }
        public string EditUserApi(string BaseUrl)
        {
            return BaseUrl;
        }
        public string UpdateUserApi(string BaseUrl,int UserId)
        {
            return string.Format("{0}{1}", BaseUrl, UserId);
        }
        public string UpdateUserProfileApi(string BaseUrl)
        {
            return  BaseUrl;
        }
        public string GetUserProfileApi(string BaseUrl)
        {
            return BaseUrl;
        }
    }
}
