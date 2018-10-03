using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.ResponseModel
{
  public  class BookAppointmentResponseModel
    {
        public Response Response { get; set; }
    }
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
    }
  
}
