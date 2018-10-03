using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.RequestModel
{
  public class SearchRequestModel
    {
        public int catId { get; set; }

        public string searchKey { get; set; }
    }
}
