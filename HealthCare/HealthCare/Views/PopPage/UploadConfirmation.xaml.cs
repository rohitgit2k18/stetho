using HealthCare_API.ApiHandler;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using HealthCare.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCare.Models;
using HealthCare_API.Model;
using HealthCare.Interface;
using Plugin.Media.Abstractions;
using HealthCare.Views;

namespace HealthCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadConfirmation : PopupPage
    {
        private UploadProfileRequest _objUploadProfileRequest;
        private UploadProfileResponse _objUploadProfileResponse;
        private GetProfilePicResponseModel _objGetProfilePicResponseModel;
        private RestApi _apiServices;
        private MediaFile _mediaFile;
        private string _baseUrl;
        private string _baseUrlGetProfile;
        private UploadProfileBase64Req _objUploadProfileBase64Req;
        private HeaderModel _objHeaderModel;


        //public UploadConfirmation(UploadProfileBase64Req objUploadProfileBase64Req)
        public UploadConfirmation(MediaFile mediafile)
        {
            InitializeComponent();
            _objUploadProfileRequest = new UploadProfileRequest();
            _objUploadProfileResponse = new UploadProfileResponse();
            _objGetProfilePicResponseModel = new GetProfilePicResponseModel();
            _apiServices = new RestApi();
            _mediaFile = mediafile;
            _baseUrl = Settings.Url + Domain.UpdateUserProfileApiConstant;
           // _objUploadProfileBase64Req = objUploadProfileBase64Req;
             _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            _baseUrlGetProfile = Settings.Url + Domain.GetUserProfileApiConstant;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;


        }
        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            //await Task.WhenAll(
            //    UsernameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //    UsernameEntry.FadeTo(1),
            //    (new Func<Task>(async () =>
            //    {
            //        await Task.Delay(200);
            //        await Task.WhenAll(
            //            PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //            PasswordEntry.FadeTo(1));

            //    }))());

            //await Task.WhenAll(
            //    CloseImage.FadeTo(1),
            //    CloseImage.ScaleTo(1, easing: Easing.SpringOut),
            //    CloseImage.RotateTo(0),
            //    LoginButton.ScaleTo(1),
            //    LoginButton.FadeTo(1));
        }

        //protected async override Task OnDisappearingAnimationBegin()
        //{
        //    var taskSource = new TaskCompletionSource<bool>();

        //    var currentHeight = FrameContainer.Height;

        //    //await Task.WhenAll(
        //    //    UsernameEntry.FadeTo(0),
        //    //    PasswordEntry.FadeTo(0),
        //    //    LoginButton.FadeTo(0));

        //    FrameContainer.Animate("HideAnimation", d =>
        //    {
        //        FrameContainer.HeightRequest = d;
        //    },
        //    start: currentHeight,
        //    end: 170,
        //    finished: async (d, b) =>
        //    {
        //        await Task.Delay(300);
        //        taskSource.TrySetResult(true);
        //    });

        //    await taskSource.Task;
        //}

        //private async void OnLogin(object sender, EventArgs e)
        //{
        //    var loadingPage = new LoadingPopupPage();
        //    await Navigation.PushPopupAsync(loadingPage);
        //    await Task.Delay(2000);
        //    await Navigation.RemovePopupPageAsync(loadingPage);
        //    await Navigation.PushPopupAsync(new LoginSuccessPopupPage());
        //}

        private void OnNoButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            //LoginButton1.BackgroundColor = Color.Blue;
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
            // await  App.NavigationPage.Navigation.PushAsync(new AppointmentBookingPage(), true);
        }

        private void YesButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                UploadImageToDB();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            //LoginButton.BackgroundColor = Color.Blue;
            //var closer = DependencyService.Get<IAndroidMethods>();
            //if (closer != null)
            //    closer.closeApplication();
            // App.NavigationPage.Navigation.PushAsync() 
        }

        private async void UploadImageToDB()
        {
            try
            {
                await Navigation.PushPopupAsync(new LoadingPopPage());
                _objUploadProfileResponse = await _apiServices.UpdateUserProfileAsync(new Get_API_Url().UpdateUserProfileApi(_baseUrl), true, _objHeaderModel, _mediaFile);
                if (_objUploadProfileResponse.Response.StatusCode == 200)
                {

                    _objGetProfilePicResponseModel = await _apiServices.GetUserprofileAsync(new Get_API_Url().GetUserProfileApi(_baseUrlGetProfile), true, _objHeaderModel, _objGetProfilePicResponseModel);
                    if (_objGetProfilePicResponseModel.Response.StatusCode == 200)
                    {
                        Settings.ProfilePicture = _objGetProfilePicResponseModel.Response.ProfilePicture;
                        await App.NavigationPage.Navigation.PushAsync(new UserProfilePage());
                        //DependencyService.Get<IToast>().ShowToast(_objGetProfilePicResponseModel.Response.Message);
                       
                    }
                     DependencyService.Get<IToast>().ShowToast(_objUploadProfileResponse.Response.message);
                    await Navigation.PopAllPopupAsync();
                }
                else
                {
                    DependencyService.Get<IToast>().ShowToast(_objUploadProfileResponse.Response.message);
                    await Navigation.PopAllPopupAsync();
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}