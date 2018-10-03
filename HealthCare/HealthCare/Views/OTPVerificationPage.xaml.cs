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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPVerificationPage : ContentPage
    {
        #region   varibale Declaration

        private string _baseUrl;
        private RestApi _apiServices;      
        private OTPResponse _objOTPResponseModel;
        private OTPVerificationResponseModel _objOTPVerificationResponseModel;       
        #endregion

        public OTPVerificationPage(OTPResponse ObjOTPResponseModel)
        {
            InitializeComponent();
            _objOTPResponseModel = ObjOTPResponseModel;
            _objOTPVerificationResponseModel = new OTPVerificationResponseModel();
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.OTPVerificationApiConstant;
            NavigationPage.SetHasNavigationBar(this, false);          
            otpDescTxt.Text = "we have sent you an OTP to"+" "+Settings.PhoneNo+" "+ "Please enter the same here!";
            entryoOtpNo.Completed += (sender, e) =>
            {
                btnOtpSubmit_Clicked(sender, e);
            };
        }
        private async void lbleditPhone_Click(object sender, EventArgs e)
        {
            var otherPage = new MainPage();
            var homePage = App.NavigationPage.Navigation.NavigationStack.First();
            App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
            await App.NavigationPage.PopToRootAsync(false);
        }
        private async void btnOtpSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                var otptxt = entryoOtpNo.Text;
                _objOTPResponseModel.OTP =Convert.ToInt32(otptxt);
                if (string.IsNullOrEmpty(otptxt))
                {                  
                    DependencyService.Get<IToast>().ShowToast("Please Entr 4 digit OTP here!");
                }
                else
                {
                    await Navigation.PushPopupAsync(new LoadingPopPage());
                    _objOTPVerificationResponseModel = await _apiServices.OTPAsync(new Get_API_Url().OTPApi(_baseUrl), false, new HeaderModel(), _objOTPResponseModel);
                    var Result = _objOTPVerificationResponseModel.Response;
                    if (Result.StatusCode==200)
                    {
                        if(Result.DOB!=null)
                        {
                           DateTime dts=  Convert.ToDateTime(Result.DOB.ToString());
                            //DateTime date = Convert.ToDateTime(dts, CultureInfo.InvariantCulture);                           
                            //DateTime dt = DateTime.Parse(dts,  CultureInfo.InvariantCulture);
                            string dateOfBirth = dts.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                            Settings.DOB = dateOfBirth;
                        }
                        else
                        {
                            Settings.DOB = Result.DOB.ToString();
                        }                                      
                       // Settings.UserName = Result.UserName;
                        Settings.Name = Result.Name;
                        Settings.Id = Result.Id;                                                                          
                        Settings.ProfilePicture = Result.ProfilePicture;
                        //"https://media.licdn.com/mpr/mpr/shrinknp_200_200/AAEAAQAAAAAAAAV7AAAAJDZjNjkwZDg4LTEyMjktNGJjYy05ZjIyLWE5M2VhMjJkNDE2ZA.jpg"; //"http://www.pathlab360.com/images/doctor-single.jpg"; //
                        Settings.Address = Result.Address;
                        Settings.RoleId = Result.RoleId;
                        Settings.TokenCode = Result.TokenCode;
                        Settings.UserPin = Result.UserPin;
                        Settings.PhoneNo = Result.PhoneNo;
                        Settings.Gender = Result.Gender;
                        Settings.Email = Result.Email;
                        Settings.IsLoggedIn = true;
                       
                       // DependencyService.Get<IPushNotificationRegister>().ExtractTokenAndRegister();
                       // DependencyService.Get<IToast>().ShowToast(Result.Message);
                        var otherPage = new MenuPage();
                        var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                        App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
                      await  App.NavigationPage.PopToRootAsync(false);
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().ShowToast(Result.Message);
                        await Navigation.PopAllPopupAsync();
                    }
                    
                }
            }
           catch(Exception ex)
            {
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
        }

        private void entryoOtpNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > 4)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, 4);
        }
    }
}