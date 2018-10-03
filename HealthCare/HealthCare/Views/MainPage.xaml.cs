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

namespace HealthCare.Views
{
    public partial class MainPage : ContentPage
    {

        #region   varibale Declaration

        private string _baseUrl;     
        private RestApi _apiServices;
        private OTPRequestModel _objOTPRequestModel;
        private OTPResponseModel _objOTPResponseModel;
        #endregion


        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.LoginApiConstant;
            _objOTPResponseModel = new OTPResponseModel();
            _objOTPRequestModel = new OTPRequestModel();
            entryPhoneNo.Completed += (sender, e) =>
             {
                 btnphoneNoSubmit_Clicked(sender, e);
             };
        }



        private async void btnphoneNoSubmit_Clicked(object sender, EventArgs e)
        {

            try
            {
                var txtEntry = entryPhoneNo.Text;
                _objOTPRequestModel.PhoneNo = txtEntry;
                _objOTPRequestModel.DeviceId = "1";
                _objOTPRequestModel.DeviceType = "Android";
                if (string.IsNullOrEmpty(txtEntry) || txtEntry.Length < 10 || txtEntry.Length>15)
                {
                    DependencyService.Get<IToast>().ShowToast("Your Phone Number should be of 10 digits!!");
                }
                else
                {
                    await Navigation.PushPopupAsync(new LoadingPopPage());
                    _objOTPResponseModel = await _apiServices.LoginAsync(new Get_API_Url().LoginApi(_baseUrl), false, new HeaderModel(), _objOTPRequestModel);
                    var Result = _objOTPResponseModel.Response;
                    if (Result.StatusCode == 200)
                    {
                        Settings.PhoneNo = txtEntry;
                       // DependencyService.Get<IToast>().ShowToast(Result.Message);
                        await App.NavigationPage.Navigation.PushAsync(new OTPVerificationPage(Result));
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().ShowToast(Result.Message);
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

        private void entryPhoneNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > 15)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, 15);
        }

        private  void SignUp_Click(object sender, EventArgs e)
        {
            try
            {
              // App.NavigationPage.Navigation.PushAsync(new SignUpPage());
                App.NavigationPage.Navigation.PushAsync(new NewRegisterUserPage());
            }
           catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
