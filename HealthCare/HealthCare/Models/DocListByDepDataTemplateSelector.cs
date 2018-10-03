using HealthCare_API.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCare.Models
{
   public class DocListByDepDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LoadDoctorByDeptTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is DoctorByDepartmentList)
            {
                return LoadDoctorByDeptTemplate;
            }
            else
                return null;

        }
    }
}

