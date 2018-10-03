using HealthCare_API.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterOptionPage : ContentPage
    {
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "REPORT FILTER");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        private ReportRequestModel _objReportRequestModel;
        public FilterOptionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _objReportRequestModel = new ReportRequestModel();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(entryClinicname.Text))
                {
                    _objReportRequestModel.ClinicName = string.Empty;                  
                }
                else if(!string.IsNullOrEmpty(entryClinicname.Text))
                {
                    _objReportRequestModel.ClinicName = entryClinicname.Text;
                }
                 if (string.IsNullOrEmpty(entryDocname.Text))
                {
                   _objReportRequestModel.DoctorName = string.Empty;                  
                }
                else if (!string.IsNullOrEmpty(entryDocname.Text))
                {
                    _objReportRequestModel.DoctorName = entryDocname.Text;
                }
                _objReportRequestModel.Limit = 15;
                _objReportRequestModel.OffSet = 0;
                _objReportRequestModel.UserId = Settings.Id;          
                App.NavigationPage.Navigation.PushAsync(new ReportsPage(_objReportRequestModel));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                App.NavigationPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private void Switchdate_Toggled(object sender, ToggledEventArgs e)
        {
            _objReportRequestModel.IsOrderByDate = e.Value;
        }

        private void switchOrder_Toggled(object sender, ToggledEventArgs e)
        {
            _objReportRequestModel.OrderByName = e.Value;
        }
    }
}