using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.RequestModel
{
  public  class DoctorListRequestModel
    {
        public long DepartmentId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleStartShift { get; set; }
    }
}
