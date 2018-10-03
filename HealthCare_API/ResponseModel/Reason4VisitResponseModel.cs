using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
  public  class Reason4VisitResponseModel
    {
        public Reason4VisitData Response { get; set; }
    }
    public class ReasonList
    {
        public int Id { get; set; }
        public string ReasonForVisit { get; set; }
    }

    public class Reason4VisitData
    {
        public Reason4VisitData()
        {
            ReasonList = new List<ResponseModel.ReasonList>();
        }
        public List<ReasonList> ReasonList { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
