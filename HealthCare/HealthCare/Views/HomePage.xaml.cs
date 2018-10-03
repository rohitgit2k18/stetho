using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCare_API.ApiHandler;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using HealthCare_API.Model;
using HealthCare.Models;
using Rg.Plugins.Popup.Extensions;
using HealthCare.Interface;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "HOME");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        private string _baseUrl;
        private string _baseUrlPrescription;
        private string _baseUrlReports;
        private RestApi _apiServices;
        private AppointmentRequestModel _objAppointmentRequestModel;
        private AppointmentResponseModel _objAppointmentResponseModel;
        private PrescriptionRequestModel _objPrescriptionRequestModel;
        private PrescriptionResponseModel _objPrescriptionResponseModel;
        private ReportRequestModel _objReportRequestModel;
        private ReportResponseModel _objReportResponseModel;
        private HeaderModel _objHeaderModel;

        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.AppointmentApiConstant;
            _baseUrlPrescription= Settings.Url + Domain.PrescriptionApiConstant;
            _baseUrlReports= Settings.Url + Domain.ReportApiConstant;
            _objAppointmentResponseModel = new AppointmentResponseModel();
            _objAppointmentRequestModel = new AppointmentRequestModel();
            _objPrescriptionRequestModel = new PrescriptionRequestModel();
            _objPrescriptionResponseModel = new PrescriptionResponseModel();
            _objReportRequestModel = new ReportRequestModel();
            _objReportResponseModel = new ReportResponseModel();
            _objHeaderModel = new HeaderModel();
            //App.DetailPage = this;
            _objHeaderModel.OTPToken = Settings.TokenCode;
          
           
        }

        protected override  void OnAppearing()
        {
            try
            {
                lblUserName.Text = Settings.Name;
                lblUserLocation.Text = "Location: " + Settings.Address;
              //  lblUserPin.Text = "PIN: " + Settings.UserPin.ToString();
                if (Settings.ProfilePicture == string.Empty)
                {
                    UserProfilePic.Source = "user_Image.png";
                }
                else
                {
                    UserProfilePic.Source = Settings.ProfilePicture;
                }
               
                // Navigation.PushPopupAsync(new LoadingPopPage());
                LoadUpcomingAppointment(1,string.Empty);
                LoadPrescription(string.Empty);
                LoadReportByFilter();
                Navigation.PopAllPopupAsync();
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
          
        }
        private async void editProfile_Click(object sender, EventArgs e)
        {
            
            await App.NavigationPage.Navigation.PushAsync(new UserProfilePage());
        }
        private async void Upcoming_Appointment_Click(object sender, EventArgs e)
        {
            await App.NavigationPage.Navigation.PushAsync(new AppointmentPage());
        }
        private async void Reports_Click(object sender, EventArgs e)
        {
            await App.NavigationPage.Navigation.PushAsync(new ReportsPage(_objReportRequestModel));
        }

        private async void LoadUpcomingAppointment(int ListType,string Keyword)
        {
            try
            {
                _objAppointmentRequestModel.Key = Keyword;
                _objAppointmentRequestModel.UserId = Settings.Id;
                _objAppointmentRequestModel.ListType = ListType;
                _objHeaderModel.OTPToken = Settings.TokenCode;               
                _objAppointmentResponseModel = await _apiServices.LoadAppointmentListAsync(new Get_API_Url().AppointmentListApi(_baseUrl), true, _objHeaderModel, _objAppointmentRequestModel);
                var Result = _objAppointmentResponseModel.Response;
                if (Result.StatusCode == 200)
                {
                    //listUpcomingAppointment.ItemsSource = Result.AppointmentList;
                    var result = Result.AppointmentList.FirstOrDefault() as AppointmentList;
                    if(result!=null)
                    {
                        Appointmentdate.Text = result.AppointmentDate + ",";
                        AppointmentTme.Text = result.AppointmentTime;
                        lblreason4Visit.Text = result.ReasonForVisit;
                        LableHospitalName.Text = result.HospitalName;
                    }
                    // DocProfilePic.Source = result.DoctorProfilePic;
                  
                  //  DependencyService.Get<IToast>().ShowToast(Result.Message);
                   
                }
                else
                {
                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                  //  await Navigation.PopAllPopupAsync();
                }

            }
            catch (Exception ex)
            {
                //await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }

        public async void LoadPrescription(string Keyword)
        {
            try
            {
                // await Navigation.PushPopupAsync(new LoadingPopPage());
                _objPrescriptionRequestModel.key = Keyword;
                 _objPrescriptionResponseModel = await _apiServices.GetPrescriptionListAsync(new Get_API_Url().PrescriptionListApi(_baseUrlPrescription), true, _objHeaderModel, _objPrescriptionRequestModel);
                var Result = _objPrescriptionResponseModel.Response;
                if (Result.StatusCode == 200)
                {
                    // listprescriptionListByDoc.ItemsSource = Result.PrescriptionList;
                    var result = Result.PrescriptionList.FirstOrDefault() as PrescriptionList;
                    if(result!=null)
                    {
                        ListPillsReminder.ItemsSource = result.TabletList;
                        pillremindertime.Text = result.PrescriptionTime;
                    }
                      
                   // LblAllergy.Text
                   // DependencyService.Get<IToast>().ShowToast(Result.Message);
                   // await Navigation.PopAllPopupAsync();
                }
                else
                {
                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                   // await Navigation.PopAllPopupAsync();
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
               // await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }

        public async void LoadReportByFilter()
        {
            try
            {
                _objReportRequestModel.ClinicName = "";
                _objReportRequestModel.DoctorName = "";
                _objReportRequestModel.IsOrderByDate = true;
                _objReportRequestModel.OrderByName = false;
                _objReportRequestModel.Limit = 15;
                _objReportRequestModel.OffSet = 0;
                _objReportRequestModel.UserId = Settings.Id;
                //  await Navigation.PushPopupAsync(new LoadingPopPage());
                _objReportResponseModel = await _apiServices.GetReportListAsync(new Get_API_Url().ReportListApi(_baseUrlReports), true, _objHeaderModel, _objReportRequestModel);
                var Result = _objReportResponseModel.Response;
                if (Result.StatusCode == 200)
                {
                    //listReportListByFilter.ItemsSource = Result.ReportListPatient;
                    var result = Result.ReportListPatient.FirstOrDefault() as ReportListPatient;
                    if(result!=null)
                    {
                        LblReportdate.Text = result.ReportDateTime + ",";
                        LblReporttime.Text = result.ReportTime;
                    }
                    
                   // DependencyService.Get<IToast>().ShowToast(Result.Message);
                    // await App.NavigationPage.Navigation.PushAsync(new OTPVerificationPage(Result));
                    await Navigation.PopAllPopupAsync();
                }
                else
                {
                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                    await Navigation.PopAllPopupAsync();
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }
    }
}