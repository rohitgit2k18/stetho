using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using HealthCare_API.ApiHandler;
using HealthCare_API.RequestModel;
using HealthCare_API.Model;
using HealthCare_API.ResponseModel;
using HealthCare.Models;
using HealthCare.Interface;
using HealthCare.ViewModels;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage :MasterDetailPage
    {
        #region   varibale Declaration
        public static int PageId;
        private string _baseUrl;       
        private RestApi _apiServices;
        private LogoutRequestModel _objLogoutRequestModel;
        private LogoutResponseModel _objLogoutResponseModel;
        private NavigationServiceViewModel _objNavigationServiceViewModel;
        private HeaderModel _objHeaderModel;
        #endregion
        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MasterBehavior = MasterBehavior.Popover;
            imgProfile.Source = Settings.ProfilePicture;
            lblUserName.Text = Settings.Name;
            lblUserLocation.Text = Settings.Address;
          //  lblUserPin.Text = "PIN: "+Settings.UserPin.ToString();
            _objNavigationServiceViewModel = new NavigationServiceViewModel();
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.LOgOutApiConstant;
            _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            _objLogoutRequestModel = new LogoutRequestModel();
            _objLogoutResponseModel = new LogoutResponseModel();          
            NavigationList.ItemsSource = _objNavigationServiceViewModel.NavigationMenuList;
        }

        private void imgedit_Click(object sender, EventArgs e)
        {
            //var details = new NavigationPage(new UserProfilePage());
            //App.Navigation = details.Navigation;
            //Detail = details;
            //details.Title = "UserProfilePage";
            //IsPresented = false;
            //details.BarBackgroundColor = Color.FromHex("#022d71");

            var details = new NavigationPage(new UserProfilePage());
            App.DetailPage = new UserProfilePage();
            details.Title = "UserProfilePage";
            App.Navigation = details.Navigation;
            Detail = details;
            IsPresented = false;
        }
        //private void lblHome_Click(object sender, EventArgs e)
        //{
        //    var detail = new NavigationPage(new HomePage());
        //    App.Navigation = detail.Navigation;
        //    Detail = detail;
        //    IsPresented = false;
        //    detail.BarBackgroundColor = Color.FromHex("#022d71");
        //}
        //private void lblPrescriptions_Click(object sender, EventArgs e)
        //{

        //    var detail = new NavigationPage(new PrescriptionPage());
        //    App.Navigation = detail.Navigation;
        //    Detail = detail;
        //    IsPresented = false;
        //    detail.BarBackgroundColor = Color.FromHex("#022d71");
        //}
        //private void lblAppointments_Click(object sender, EventArgs e)
        //{
        //    var detail =  new NavigationPage(new AppointmentPage());
        //    App.Navigation = detail.Navigation;
        //    Detail = detail;
        //    IsPresented = false;
        //    detail.BarBackgroundColor = Color.FromHex("#022d71");

        //}
        private async void Logout()
        {
            try
            {               
                await Navigation.PushPopupAsync(new LoadingPopPage());
                _objLogoutResponseModel = await _apiServices.LogOutAsync(new Get_API_Url().LogOutApi(_baseUrl), true, _objHeaderModel, _objLogoutRequestModel);
                var Result = _objLogoutResponseModel.Response;
                if (Result.StatusCode == 200)
                {
                    Settings.IsLoggedIn = false;
                    Settings.TokenCode = string.Empty;
                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                    var otherPage = new MainPage();
                    var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                    App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
                    await  App.NavigationPage.PopToRootAsync(false);
                   // await App.NavigationPage.Navigation.PushAsync(new MainPage());
                    await Navigation.PopAllPopupAsync();
                }
                else
                {
                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                    await Navigation.PopAllPopupAsync();
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                await Navigation.PopAllPopupAsync();
            }
        
        }

        /// <summary>
        ///  Item List Clicked Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            (sender as ListView).SelectedItem = null;
            var selectedValue = e.SelectedItem as NavigationViewModel;
            if (e.SelectedItem != null)
            {
                //Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                //for Logout Page
                switch (selectedValue.Id)
                {
                    case 1:                        
                        var detail = new NavigationPage(new HomePage());
                        App.DetailPage = new HomePage();
                        detail.Title = "HomePage";
                        App.Navigation = detail.Navigation;
                        Detail = detail;
                        IsPresented = false;
                        break;
                    case 2:
                        var detail1 = new NavigationPage(new PrescriptionPage());
                        App.DetailPage = new PrescriptionPage();
                        detail1.Title = "PrescriptionPage";
                        App.Navigation = detail1.Navigation;
                        Detail = detail1;
                        IsPresented = false;
                        break;
                    case 3:
                        var detail2 = new NavigationPage(new AppointmentPage());
                        App.DetailPage = new AppointmentPage();
                        detail2.Title = "AppointmentPage";
                        App.Navigation = detail2.Navigation;
                        Detail = detail2;
                        IsPresented = false;
                        break;
                    case 4:
                        Logout();
                        break;                 
                    default:

                        break;

                }

               

                
            }


        }
    }
}