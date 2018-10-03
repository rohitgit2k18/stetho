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

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentPage : ContentPage
    {
        #region   varibale Declaration
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "APPOINTMENTS");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        private string _baseUrl;
        private RestApi _apiServices;      
        private AppointmentRequestModel _objAppointmentRequestModel;
        private AppointmentResponseModel _objAppointmentResponseModel;
        private HeaderModel _objHeaderModel;
        #endregion
        public AppointmentPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.AppointmentApiConstant;
            _objAppointmentResponseModel = new AppointmentResponseModel();
            _objAppointmentRequestModel = new AppointmentRequestModel();
            _objHeaderModel = new HeaderModel();
            LoadUpcomingAppointment(1,string.Empty);
           


        }

        private async void LoadUpcomingAppointment(int ListType,string Keyword)
        {
            try
            {
                _objAppointmentRequestModel.Key = Keyword;
                _objAppointmentRequestModel.UserId = Settings.Id; 
                _objAppointmentRequestModel.ListType = ListType;
                _objHeaderModel.OTPToken = Settings.TokenCode;
                await Navigation.PushPopupAsync(new LoadingPopPage());
                _objAppointmentResponseModel = await _apiServices.LoadAppointmentListAsync(new Get_API_Url().AppointmentListApi(_baseUrl), true, _objHeaderModel, _objAppointmentRequestModel);
                var Result = _objAppointmentResponseModel.Response;
                if (Result.StatusCode == 200)
                {
                    if (Result.AppointmentList.Count > 0)
                    {
                        if (ListType == 1)
                        {
                            listUpcomingAppointment.IsVisible = true;
                            listPastAppointment.IsVisible = false;
                            GridPastAppt.IsVisible = false;
                            listUpcomingAppointment.ItemsSource = Result.AppointmentList;

                        }
                        else
                        {

                            listPastAppointment.IsVisible = true;
                            listUpcomingAppointment.IsVisible = false;
                            GridPastAppt.IsVisible = true;
                            listPastAppointment.ItemsSource = Result.AppointmentList;


                        }
                        //listUpcomingAppointment.ItemsSource = Result.AppointmentList;
                        // DependencyService.Get<IToast>().ShowToast(Result.Message);
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().ShowToast("No Data To Display!");
                        listUpcomingAppointment.ItemsSource = null;
                        listPastAppointment.ItemsSource = null;
                        await Navigation.PopAllPopupAsync();
                    }
                }
                else
                {
                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                    await Navigation.PopAllPopupAsync();
                }

            }
            catch (Exception ex)
            {
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }

        private  void btnUpcomingAppointmnt_Clicked(object sender, EventArgs e)
        {
            SelectedTab(1);
            LoadUpcomingAppointment(1,string.Empty);
        }

        private void btnPastAppointmnt_Clicked(object sender, EventArgs e)
        {
            SelectedTab(2);
            LoadUpcomingAppointment(2,string.Empty);
        }
        private void SelectedTab( int tabNo)
        {
            if(tabNo==1)
            {
                gridUpComing.BackgroundColor = Color.FromHex("#3782BB");
                lblUpComing.TextColor = Color.White;
                lblPast.TextColor = Color.Gray;
                gridPast.BackgroundColor = Color.FromHex("#FFFFFF");
            }
            else
            {
                gridUpComing.BackgroundColor = Color.FromHex("#FFFFFF");
                lblUpComing.TextColor = Color.Gray;
                lblPast.TextColor = Color.White;
                gridPast.BackgroundColor = Color.FromHex("#3782BB");
            }

        }

        private async void SearchAppintment_SearchButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
            var key = SearchAppintment.Text.ToLower();
            if (string.IsNullOrEmpty(key))
            {
                if (_objAppointmentRequestModel.ListType == 1)
                {
                    LoadUpcomingAppointment(1, string.Empty);
                }
                else
                {
                    LoadUpcomingAppointment(2, string.Empty);
                }
            }
            else
            {
                if (_objAppointmentRequestModel.ListType == 1)
                {
                    listUpcomingAppointment.ItemsSource = _objAppointmentResponseModel.Response.AppointmentList.Where(keyword => keyword.DoctorName.ToLower().Contains(key) || keyword.HospitalName.ToLower().Contains(key) || keyword.ReasonForVisit.ToLower().Contains(key));
                }
                else
                {
                    listPastAppointment.ItemsSource = _objAppointmentResponseModel.Response.AppointmentList.Where(keyword => keyword.DoctorName.ToLower().Contains(key) || keyword.HospitalName.ToLower().Contains(key) || keyword.ReasonForVisit.ToLower().Contains(key));
                }
            }
            await Navigation.PopAllPopupAsync();
        }
    }
}