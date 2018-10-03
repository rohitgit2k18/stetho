using HealthCare.Helpers;
using HealthCare.Interface;
using HealthCare.Models;
using HealthCare_API.ApiHandler;
using HealthCare_API.Model;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentBookingPage : ContentPage
    {
        #region   varibale Declaration
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "BOOKING");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        private string _baseUrl;
        public string VisitingReason;
        private string _reason4VisitUrl;
        private string _baseurlDeptList;
        private string _baseUrlDocList;
        private RestApi _apiServices;
        private int DeptId;
        private BookAppointmentRequestModel _objBookAppointmentRequestModel;
        private BookAppointmentResponseModel _objBookAppointmentResponseModel;
        private DoctorListRequestModel _objDoctorListRequestModel;
        private DoctorListResponseModel _objDoctorListResponseModel;
        private DepartmentListRequestModel _objDepartmentListRequestModel;
        private DepartmentListResponseModel _objDepartmentListResponseModel;
        private Reason4VisitResponseModel _objReason4VisitResponseModel;
        private HeaderModel _objHeaderModel;
        TimeSpan shiftstarttime;
        DateTime BookingDate;
        #endregion
        #region Constructor
        public AppointmentBookingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _apiServices = new RestApi();
            _objBookAppointmentRequestModel = new BookAppointmentRequestModel();
            _objBookAppointmentResponseModel = new BookAppointmentResponseModel();
            _objDepartmentListRequestModel = new DepartmentListRequestModel();
            _objDepartmentListResponseModel = new DepartmentListResponseModel();
            _objDoctorListRequestModel = new DoctorListRequestModel();
            _objDoctorListResponseModel = new DoctorListResponseModel();
            _objReason4VisitResponseModel = new Reason4VisitResponseModel();
            _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            
            _baseurlDeptList = Settings.Url + Domain.DepartmentApiConstant;
            _baseUrlDocList = Settings.Url + Domain.DoctorDetailApiConstant;
            _reason4VisitUrl = Settings.Url + Domain.GetReasonForVisitApiConstant;
           
            GetReason4Visit();
            listDoctorListByDept.ItemSelected += ListDoctorListByDept_ItemSelected;
        }
        #endregion
        #region Events & Methods
        private void ListDoctorListByDept_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var appointmentTime = AppointmentTime.Time;
            var Reason4Visit = VisitingReason;
            var appintmentDate = dateOfAppointment.Date;
            var docrec = e.SelectedItem as DoctorByDepartmentList;
           // _objBookAppointmentRequestModel.AppointmentId = ;
            _objBookAppointmentRequestModel.ReasonForVisit = Reason4Visit;
            _objBookAppointmentRequestModel.AppointmentDate = appintmentDate.ToString("MM/dd/yyyy");
            _objBookAppointmentRequestModel.AppointmentTime = appointmentTime.ToString();
            _objBookAppointmentRequestModel.Illness = illNessAutoSuggestion.Text;
            _objBookAppointmentRequestModel.DoctorId = docrec.Id;
            _objBookAppointmentRequestModel.PatientId = Settings.Id;
            _objBookAppointmentRequestModel.UserId = Settings.Id;
            _objBookAppointmentRequestModel.ProfilePicture = docrec.ProfilePicture;
            _objBookAppointmentRequestModel.DocName = docrec.DoctorName;
            _objBookAppointmentRequestModel.dept = docrec.DepartmentName;
            _objBookAppointmentRequestModel.hospitalname = docrec.HospitalName;
            _objBookAppointmentRequestModel.Qualification = docrec.QualificationName;
            //  App.NavigationPage.Navigation.PushAsync(new DoctorDetailViewPage(_objBookAppointmentRequestModel), true);
            Navigation.PushPopupAsync(new BookingConfirmationPage(_objBookAppointmentRequestModel), true);
        }

        //private async void SendAppointmentBooking()
        //{
        //    try
        //    {
        //        var Reason4Visit = txtreasonforvisit.Text;
        //        var Illness = illNessAutoSuggestion.Text;                
        //        if (string.IsNullOrEmpty(Reason4Visit) || string.IsNullOrEmpty(Illness))
        //        {
        //            DependencyService.Get<IToast>().ShowToast("The Required field is Empty!");
        //        }
        //        else
        //        {
        //            await Navigation.PushPopupAsync(new LoadingPopPage());
        //            _objBookAppointmentResponseModel= await _apiServices.BookAppointmentAsync(new Get_API_Url().BookAppointmentListApi(_baseUrl), true, _objHeaderModel, _objBookAppointmentRequestModel);
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
        private async void GetReason4Visit()
        {
            try
            {
               // await Navigation.PushPopupAsync(new LoadingPopPage());
                _objReason4VisitResponseModel = await _apiServices.Reason4VisitAsync(new Get_API_Url().ReasonForVisitApi(_reason4VisitUrl), true, _objHeaderModel, _objReason4VisitResponseModel);
                var _result = _objReason4VisitResponseModel.Response;
                if (_result.StatusCode == 200)
                {
                    txtreasonforvisit.ItemsSource = _result.ReasonList;                    
                   // await Navigation.PopAllPopupAsync();
                    // DependencyService.Get<IToast>().ShowToast(_result.Message);
                    GetDepartmentList();
                }
                else
                {
                  //  await Navigation.PopAllPopupAsync();
                    DependencyService.Get<IToast>().ShowToast(_result.Message);
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private async void GetDepartmentList()
        {
            try
            {
                await Navigation.PushPopupAsync(new LoadingPopPage());
                _objDepartmentListResponseModel= await _apiServices.DepartmentListAsync(new Get_API_Url().DepartmentListApi(_baseurlDeptList), true, _objHeaderModel, _objDepartmentListRequestModel);
                var _result = _objDepartmentListResponseModel.Response;
                if (_result.StatusCode==200)
                {
                    illNessAutoSuggestion.DataSource = _result.DepartmentList;
                    illNessAutoSuggestion.DisplayMemberPath = "Name";
                    illNessAutoSuggestion.SelectedValuePath = "Id";
                    await Navigation.PopAllPopupAsync();
                   // DependencyService.Get<IToast>().ShowToast(_result.Message);
                }
                else
                {
                    await Navigation.PopAllPopupAsync();
                    DependencyService.Get<IToast>().ShowToast(_result.Message);
                }
               
            }
            catch(Exception ex)
            {
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }
       
        private async void btnLoadDoctor_Clicked(object sender, EventArgs e)
        {
            try
            {
                var Reason4Visit = VisitingReason;
                var Illness = illNessAutoSuggestion.Text;
              
                //string[] splitvalue1 = x.Split('{');
                //string[] splitvalue2 = splitvalue1[1].Split('}');
                //string final = splitvalue2[0];
                if (string.IsNullOrEmpty(Reason4Visit) || string.IsNullOrEmpty(Illness))
                {
                    DependencyService.Get<IToast>().ShowToast("Please Sleect Reason For Visit And Illness First!");
                }
                else
                {
                   // string x = illNessAutoSuggestion.SelectedValue.ToString();
                    await Navigation.PushPopupAsync(new LoadingPopPage());

                    int DeptIds = illNessAutoSuggestion.SelectedIndex;                  
                    if(DeptIds != -1)
                    {
                        shiftstarttime= AppointmentTime.Time;
                        BookingDate= dateOfAppointment.Date;
                        _objDoctorListRequestModel.DepartmentId = Convert.ToInt64(illNessAutoSuggestion.SelectedValue.ToString());
                        _objDoctorListRequestModel.ScheduleStartShift = shiftstarttime;
                        _objDoctorListRequestModel.ScheduleDate = BookingDate;
                        _objDoctorListResponseModel = await _apiServices.DoctorListByDepartmentAsync(new Get_API_Url().DoctorDetailListApi(_baseUrlDocList), true, _objHeaderModel, _objDoctorListRequestModel);
                        var _result = _objDoctorListResponseModel.Response;
                        if (_result.StatusCode == 200)
                        {
                            if (_result.DoctorByDepartmentList.Count > 0)
                            {
                                listDoctorListByDept.ItemsSource = _result.DoctorByDepartmentList;
                                await Navigation.PopAllPopupAsync();
                            }
                            else
                            {
                                DependencyService.Get<IToast>().ShowToast("Their is not any doctor available at this time slot please select another time !");
                                await Navigation.PopAllPopupAsync();
                            }
                        }                        
                            else
                        {
                            await Navigation.PopAllPopupAsync();
                            DependencyService.Get<IToast>().ShowToast(_result.Message);
                        }
                    }
                    else
                    {
                        DependencyService.Get<IToast>().ShowToast("Please Select a Valid Illness from the Illness Autosuggestion!");
                        await Navigation.PopAllPopupAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }

        private async void lblQRcode_Click(object sender, EventArgs e)
        {
            try
            {
                var scanPage = new ZXingScannerPage();
                NavigationPage.SetHasNavigationBar(scanPage, true);
                scanPage.OnScanResult += (result) => {
                    // Stop scanning
                    scanPage.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PopAsync();
                        DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };

                // Navigate to our scanner page
                await App.NavigationPage.Navigation.PushAsync(scanPage);
            }
          catch(Exception ex)
            {
                var msg = ex.Message;

            }

        }
        #endregion

        private void txtreasonforvisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var selectedReason = picker.SelectedItem as ReasonList;
                VisitingReason = selectedReason.ReasonForVisit;
            }
            else
            {
                DependencyService.Get<IToast>().ShowToast("please select Reason For Visit!");
            }
            //var selectedReason = sender as ReasonList;
            //VisitingReason = selectedReason.ReasonForVisit;
        }
    }
}