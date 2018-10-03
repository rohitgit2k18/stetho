using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCare_API.ResponseModel
{
   public class PrescriptionResponseModel 
    {
        public PrescResponse Response { get; set; }

       // public event PropertyChangedEventHandler PropertyChanged;
    }

    public class TabletList
    {
        public string TabletName { get; set; }
        public string Repetaion { get; set; }
        public string Power { get; set; }
    }

    public class PrescriptionList : INotifyPropertyChanged
    {
        public PrescriptionList()
        {
            TabletList = new List<ResponseModel.TabletList>();
        }

        public int PrescriptionId { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public string Status { get; set; }
        public List<TabletList> TabletList { get; set; }      
        public string ProfilePicture { get; set; }
        public string StatusImage { get; set; }      
        public string CreatedOn { get; set; }
        public string PrescriptionTime { get; set; }      
        public string TabName { get; set; }
        public string tabTime { get; set; }
        public int HeightReq { get; set; }     
        //public FormattedString AveragePrice
        //{
        //    get
        //    {
        //        return new FormattedString
        //        {
        //            Spans =
        //        {
        //            new Span { Text = TabName, ForegroundColor=Color.Red },
        //            new Span { Text = tabTime, ForegroundColor=Color.Blue }                   
        //        }
        //        };
        //    }
        //    set { }
        //}
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class PrescResponse : INotifyPropertyChanged
    {
        public PrescResponse()
        {
            PrescriptionList = new List<ResponseModel.PrescriptionList>();
        }
        public List<PrescriptionList> PrescriptionList { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

   
}
