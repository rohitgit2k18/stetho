using HealthCare_API.ApiHandler;
using HealthCare_API.Model;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using HealthCare.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCare.Models;
using HealthCare.Interface;
using HealthCare.Views;

namespace HealthCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingConfirmationPage : PopupPage
    {

        private string _baseUrl;
        private string _baseurlDeptList;
        private string _baseUrlDocList;
        private RestApi _apiServices;
        private int DeptId;
        private BookAppointmentRequestModel _objBookAppointmentRequestModel;
        private BookAppointmentResponseModel _objBookAppointmentResponseModel;
        DepartmentListResponseModel _objDepartmentListResponseModel;
        private HeaderModel _objHeaderModel;

        public BookingConfirmationPage(BookAppointmentRequestModel ObjBookAppointmentRequestModel)
        {
            InitializeComponent();

            _objBookAppointmentRequestModel = ObjBookAppointmentRequestModel;
           // NavigationPage.SetHasNavigationBar(this, false);
            _objBookAppointmentResponseModel = new BookAppointmentResponseModel();
            _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.BookAppoinmentApiConstant;

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
                SendAppointmentBooking();
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
            //LoginButton.BackgroundColor = Color.Blue;
            //var closer = DependencyService.Get<IAndroidMethods>();
            //if (closer != null)
            //    closer.closeApplication();
           // App.NavigationPage.Navigation.PushAsync() 
        }
        private async void SendAppointmentBooking()
        {
            try
            {
                await Navigation.PushPopupAsync(new LoadingPopPage());
                _objBookAppointmentResponseModel = await _apiServices.BookAppointmentAsync(new Get_API_Url().BookAppointmentListApi(_baseUrl), true, _objHeaderModel, _objBookAppointmentRequestModel);
                var _result = _objBookAppointmentResponseModel.Response;
                if (_result.StatusCode == 200)
                {
                    //await DisplayAlert("Info", _result.Message, "OK");
                    await App.NavigationPage.Navigation.PushAsync(new AppointmentPage(), true);
                    DependencyService.Get<IToast>().ShowToast("Your Booking Is Confirmed!!");
                    await Navigation.PopAllPopupAsync();
                }
                else
                {
                    DependencyService.Get<IToast>().ShowToast(_result.Message);
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