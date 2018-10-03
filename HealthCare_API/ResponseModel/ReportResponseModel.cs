using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
  public  class ReportResponseModel
    {
        public ReportResponse Response { get; set; }
    }
    public class ReportListPatient
    {
        public int ReportId { get; set; }
        public string ReportTitle { get; set; }
        public string AddedBy { get; set; }
        public string ReportDateTime { get; set; }                 
        public string ReportTime { get; set; }
        public string FileName { get; set; }
        public string ReportUrl { get; set; }       
        public string ReportName { get; set; }
        //    public Int64 Id { get; set; }
        //    public Int64 AppointmentId { get; set; }
        //    public int ReportType { get; set; }
        public byte[] Data { get; set; }
        //    public string FileName { get; set; }
        //    public int StatusCode { get; set; }
        //    public string Message { get; set; }
        //    public string PdfUrl { get; set; }
    }

    public class ReportResponse
    {
        public ReportResponse()
        {
            ReportListPatient = new List<ResponseModel.ReportListPatient>();
        }      
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ReportListPatient> ReportListPatient { get; set; }
    }

    //public class ReportModelResponse
    //{

    //    public ReportModelResponse()
    //    {
    //        Response = new ReportResponse();
    //    }
    //    public ReportResponse Response { get; set; }
    //}
    //public class ReportResponse
    //{
    //    public Int64 Id { get; set; }
    //    public Int64 AppointmentId { get; set; }
    //    public int ReportType { get; set; }
    //    public byte[] Data { get; set; }
    //    public string FileName { get; set; }
    //    public int StatusCode { get; set; }
    //    public string Message { get; set; }
    //    public string PdfUrl { get; set; }
    //}
}
