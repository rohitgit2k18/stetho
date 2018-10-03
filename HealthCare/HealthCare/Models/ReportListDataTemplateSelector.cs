using HealthCare_API.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCare.Models
{
   public class ReportListDataTemplateSelector :DataTemplateSelector
    {
        public DataTemplate LoadReportsTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ReportListPatient)
            {
                return LoadReportsTemplate;
            }
            else
                return null;

        }
    }
}
