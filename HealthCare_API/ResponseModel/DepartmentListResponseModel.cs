using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
  public  class DepartmentListResponseModel
    {
        public DepResponse Response { get; set; }
    }

    public class DepartmentList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DepResponse
    {
        public List<DepartmentList> DepartmentList { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

   
}
