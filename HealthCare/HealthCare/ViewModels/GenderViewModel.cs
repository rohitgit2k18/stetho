using HealthCare.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.ViewModels
{
   public class GenderViewModel
    {
        public ObservableCollection<GenderModel> GetRadioType()
        {
            var list = new ObservableCollection<GenderModel>
         {
               new GenderModel
               {
                   ID=1,
                   Name="Male"
               },
                new GenderModel
               {
                   ID=2,
                   Name="Female"
               }
          };
            return list;
        }
    }
}
