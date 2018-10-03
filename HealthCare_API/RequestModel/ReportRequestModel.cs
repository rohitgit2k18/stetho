using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.RequestModel
{
  public  class ReportRequestModel
    {
        public int UserId { get; set; }
        public bool IsOrderByDate { get; set; }
        public bool OrderByName { get; set; }
        public string DoctorName { get; set; }
        public string ClinicName { get; set; }
        public int Limit { get; set; }
        public int OffSet { get; set; }
    }
}
