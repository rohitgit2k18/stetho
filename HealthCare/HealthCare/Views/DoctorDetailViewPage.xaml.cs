using HealthCare.Models;
using HealthCare_API.ApiHandler;
using HealthCare_API.Model;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCare.Interface;
using Rg.Plugins.Popup.Extensions;


namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorDetailViewPage : ContentPage
    {
        #region   varibale Declaration
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "APPOINTMENT DETAILS");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        private string _baseUrl;
        private string _baseurlDeptList;
        private string _baseUrlDocList;
        private RestApi _apiServices;
        private int DeptId;
        private BookAppointmentRequestModel _objBookAppointmentRequestModel;
        private BookAppointmentResponseModel _objBookAppointmentResponseModel;
        DepartmentListResponseModel _objDepartmentListResponseModel;
        private HeaderModel _objHeaderModel;
        #endregion
        public DoctorDetailViewPage(BookAppointmentRequestModel ObjBookAppointmentRequestModel)
        {

            InitializeComponent();
            _objBookAppointmentRequestModel = ObjBookAppointmentRequestModel;
            NavigationPage.SetHasNavigationBar(this, false);          
            _objBookAppointmentResponseModel = new BookAppointmentResponseModel();
            _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.BookAppoinmentApiConstant;
          
        }
        protected override void OnAppearing()
        {
            try
            {
                ImageDocPic.Source = _objBookAppointmentRequestModel.ProfilePicture;
                lbldocname.Text = _objBookAppointmentRequestModel.DocName;
                LblDocQuali.Text = _objBookAppointmentRequestModel.Qualification;
                LblDept.Text = _objBookAppointmentRequestModel.dept;
                LblHospitalName.Text = _objBookAppointmentRequestModel.hospitalname;
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
        //private async void SendAppointmentBooking()
        //{
        //    try
        //    {              
        //            await Navigation.PushPopupAsync(new LoadingPopPage());
        //            _objBookAppointmentResponseModel = await _apiServices.BookAppointmentAsync(new Get_API_Url().BookAppointmentListApi(_baseUrl), true, _objHeaderModel, _objBookAppointmentRequestModel);
        //        var _result = _objBookAppointmentResponseModel.Response;
        //        if(_result.StatusCode==200)
        //        {
        //            await DisplayAlert("Info",_result.Message,"OK");
        //          await  App.NavigationPage.Navigation.PushAsync(new AppointmentPage(), true);
        //            DependencyService.Get<IToast>().ShowToast(_result.Message);
        //            await Navigation.PopAllPopupAsync();
        //        }
        //        else
        //        {
        //            DependencyService.Get<IToast>().ShowToast(_result.Message);
        //            await Navigation.PopAllPopupAsync();
        //        }              
                
        //    }
        //    catch (Exception ex)
        //    {
        //        DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
        //        await Navigation.PopAllPopupAsync();
        //        var msg = ex.Message;

        //    }
        //}
        private async void schedule_CellTapped(object sender, Syncfusion.SfSchedule.XForms.CellTappedEventArgs e)
        {
            try
            {
              //  SendAppointmentBooking();
           await Navigation.PushPopupAsync(new BookingConfirmationPage(_objBookAppointmentRequestModel));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }          
        }
    }
}