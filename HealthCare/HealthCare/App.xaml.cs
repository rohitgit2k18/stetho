using HealthCare.Helpers;
using HealthCare.Interface;
using HealthCare.Models;
using HealthCare.Views;
using HealthCare_API.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HealthCare
{
    public partial class App : Application
    {
      
        public static INavigation Navigation;
        public static Page DetailPage;
        public static NavigationPage NavigationPage { get; private set; }
        private ReportRequestModel _objReportRequestModel;
        public App()
        {
            InitializeComponent();
            Settings.Url = Domain.Url;
            _objReportRequestModel = new ReportRequestModel();
            // NavigationPage  = new  NavigationPage(new EditProfilePage());
            SetMainpage();
              // MainPage = NavigationPage;
              DetailPage = new HomePage();

        //var rs=Application.Current.Resources["xyzfield"];
        }
        private void SetMainpage()
        {
            try
            {
              
                if (!string.IsNullOrEmpty(Settings.TokenCode))
                {
                    NavigationPage = new NavigationPage(new MenuPage());
                    MainPage = NavigationPage;                   
                }
                else
                {
                    NavigationPage = new NavigationPage(new MainPage());
                    MainPage = NavigationPage;
                }
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void imgHamburger_Click(object sender, EventArgs e)
        {
            
            //var res = (App.NavigationPage.CurrentPage as MasterDetailPage).IsPresented;
            //if (res)
            //{
            //    (App.NavigationPage.CurrentPage as MasterDetailPage).IsPresented = false;
            //}
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    var x = Navigation;
                    //var otherPage = new Driver_NavigationPage();
                    //otherPage.Detail = new NavigationPage(new Driver_WorksheetPage());
                    //var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                    //App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
                    //await App.NavigationPage.PopToRootAsync(false);
                    //(App.NavigationPage.CurrentPage as MasterDetailPage).IsPresented = true;

                    Page previousPage = App.NavigationPage.Navigation.NavigationStack.Last();
                    string name = previousPage.Title;
                    //  var page = new MasterDetailPage();
                    //  var previousPage = App.NavigationPage.Navigation.NavigationStack.Last();

                    var currentpage = NavigationPage.CurrentPage;
                    //  var nextPage = previousPage;
                    //(App.NavigationPage.CurrentPage as MasterDetailPage).Detail = null;
                    ///  homePage = new HomePage();
                    //App.RootPage.IsPresented = true;
                    //RootPage = new MenuPage();
                    //App.MenuIsPresented = true;
                    //  MasterDetailPage.IsPresentedProperty.Equals(true);
                    // var masterPage = MainPage as MasterDetailPage;
                    //  masterPage.Detail = homePage;
                    //App.Current.MainPage.Navigation.PushAsync(new HomePage());

                    var otherPage = new MenuPage();
                    if (DetailPage.Title == "HomePage" && name == "MenuPage")
                    {

                    }
                    else
                    {
                        otherPage.Detail = new NavigationPage(DetailPage);
                    }

                    // var otherPage = currentpage;
                    var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                    App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
                    App.NavigationPage.PopToRootAsync(false);
                    // (App.NavigationPage.CurrentPage as MasterDetailPage).Detail = previousPage;

                    (App.NavigationPage.CurrentPage as MasterDetailPage).IsPresented = true;
                }
                else
                {                                      
                    //Page previousPage = App.NavigationPage.Navigation.NavigationStack.Last();
                    //string name = previousPage.Title;                    
                    var currentpage = NavigationPage.CurrentPage;                   
                    var otherPage = new MenuPage();
                    //if (DetailPage.Title == "HomePage" && name == "MenuPage")
                    //{

                    //}
                    //else
                    //{
                    //  //  ((NavigationPage)otherPage.Detail).PushAsync(new previousPage());
                    //    otherPage.Detail = new NavigationPage(DetailPage);
                    //}
                    // var otherPage = currentpage;
                    var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                    App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
                    App.NavigationPage.PopToRootAsync(false);
                    // (App.NavigationPage.CurrentPage as MasterDetailPage).Detail = previousPage;
                    (App.NavigationPage.CurrentPage as MasterDetailPage).IsPresented = true;
                }
               
            }
            catch(Exception ex)
            {
               
                var msg = ex.Message;
            }           
        }
        private void lblHomeMaster_Click(object sender, EventArgs e)
        {
            //    var otherPage = new HomePage();
            //    var homePage = App.NavigationPage.Navigation.NavigationStack.First();
            //    App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
            //    App.NavigationPage.PopToRootAsync(false);
            App.Current.MainPage.Navigation.PushAsync(new HomePage(),true);
            DetailPage =new HomePage();
        }
        private void lblPrescriptionMaster_Click(object sender, EventArgs e)
        {
            //var otherPage = new PrescriptionPage();
            //var homePage = App.NavigationPage.Navigation.NavigationStack.First();
            //App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
            //App.NavigationPage.PopToRootAsync(false);
            App.Current.MainPage.Navigation.PushAsync(new PrescriptionPage(),true);
            DetailPage = new PrescriptionPage();
        }
        private void imgappointment_Click(object sender, EventArgs e)
        {
           
            //var otherPage = new AppointmentPage();
            //var homePage = App.NavigationPage.Navigation.NavigationStack.First();
            //App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
            //App.NavigationPage.PopToRootAsync(false);
            // Navigation.PushAsync(new AppointmentPage());
            App.Current.MainPage.Navigation.PushAsync(new AppointmentBookingPage(),true);
            DetailPage = new AppointmentBookingPage();
        }
        private void lblReportsMaster_Click(object sender, EventArgs e)
        {
            try
            {
                _objReportRequestModel.ClinicName = "";
                _objReportRequestModel.DoctorName = "";
                _objReportRequestModel.IsOrderByDate = true;
                _objReportRequestModel.OrderByName = false;
                _objReportRequestModel.Limit = 15;
                _objReportRequestModel.OffSet = 0;
                //var otherPage = new AppointmentPage();
                //var homePage = App.NavigationPage.Navigation.NavigationStack.First();
                //App.NavigationPage.Navigation.InsertPageBefore(otherPage, homePage);
                //App.NavigationPage.PopToRootAsync(false);
                //  Navigation.PushAsync(new HomePage());
                App.Current.MainPage.Navigation.PushAsync(new ReportsPage(_objReportRequestModel), true);
                DetailPage = new ReportsPage(_objReportRequestModel);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
        private void lblHealthMonitoring_Click(object sender, EventArgs e)
        {
            DependencyService.Get<IToast>().ShowToast("This Service will be Available Soon !!");
        }
        private void notification_Click(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new NotificationPage(), true);
            DetailPage = new NotificationPage();
        }
    }
}
