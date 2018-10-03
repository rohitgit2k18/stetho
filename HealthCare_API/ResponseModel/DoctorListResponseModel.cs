using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
   public class DoctorListResponseModel
    {
        public DocResponse Response { get; set; }
    }
    public class DoctorByDepartmentList
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string HospitalName { get; set; }      
        public string ProfilePicture { get; set; }
        public string QualificationName { get; set; }
    }

    public class DocResponse
    {
        public List<DoctorByDepartmentList> DoctorByDepartmentList { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

   
}
