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
using Rg.Plugins.Popup.Extensions;
using HealthCare.Interface;
using HealthCare.Models;
using Plugin.Share;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        #region   varibale Declaration
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "REPORTS");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        private string _baseUrl;
        private string _baseurlDeptList;
        private string _baseUrlDocList;
        public IDownloadFile File;
        private RestApi _apiServices;
        private ReportListPatient _objReportListPatient;
        private ReportRequestModel _objReportRequestModel;
        private ReportResponseModel _objReportResponseModel;
        private HeaderModel _objHeaderModel;
        bool isDownloading = true;
        #endregion
        public ReportsPage(ReportRequestModel objReportRequestModel)
        {
            InitializeComponent();
            CrossDownloadManager.Current.CollectionChanged += (sender, e) =>
                System.Diagnostics.Debug.WriteLine(
                    "[DownloadManager] " + e.Action +
                    " -> New items: " + (e.NewItems?.Count ?? 0) +
                    " at " + e.NewStartingIndex +
                    " || Old items: " + (e.OldItems?.Count ?? 0) +
                    " at " + e.OldStartingIndex
                );
            NavigationPage.SetHasNavigationBar(this, false);
            _apiServices = new RestApi();
            _objReportListPatient = new ReportListPatient();
            _baseUrl = Settings.Url + Domain.ReportApiConstant;
            _objReportRequestModel = new ReportRequestModel();
            _objReportResponseModel = new ReportResponseModel();
            _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            _objReportRequestModel = objReportRequestModel;
            App.DetailPage = this;
            LoadReportByFilter();
        }

        public async void DownlodFile(String FileNmae)
        {
            await Task.Yield();
            //UserDialogs.Instance.ShowLoading("Downloading..", MaskType.Black);
            await Navigation.PushPopupAsync(new DownLoadingPage());
            await Task.Run( () =>
            {
                var downloadManager = CrossDownloadManager.Current;
                var file = downloadManager.CreateDownloadFile(FileNmae);
                downloadManager.Start(file, true);

                while (isDownloading)
                {
                      //await Task.Delay(10 * 200);
                     isDownloading = IsDownloading(file);

                }
            });
            // UserDialogs.Instance.HideLoading();
            await Navigation.PopAllPopupAsync();
            if (!isDownloading)
            {
                // await DisplayAlert("File Status", "File Downloded", "OK");
                DependencyService.Get<IToast>().ShowToast("Your file has been downloded please go to your Gallery to see your download ! ");
            }
        }

        public void AbortDownloading()
        {
            CrossDownloadManager.Current.Abort(File);
        }

        float TotalBytewritten = 0;
        public bool IsDownloading(IDownloadFile File)
        {
            //if(TotalBytewritten== File.TotalBytesWritten && File.TotalBytesWritten!=0)
            //{
            //    return false;
            //}
            //TotalBytewritten = File.TotalBytesWritten;
            if (File == null) return false;

            switch (File.Status)
            {
                case DownloadFileStatus.INITIALIZED:
                case DownloadFileStatus.PAUSED:
                case DownloadFileStatus.PENDING:
                case DownloadFileStatus.RUNNING:
                   // DependencyService.Get<IToast>().ShowToast();
                    return true;

                case DownloadFileStatus.COMPLETED:
                case DownloadFileStatus.CANCELED:
                case DownloadFileStatus.FAILED:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public async void LoadReportByFilter()
        {
            try
            {
                await Navigation.PushPopupAsync(new LoadingPopPage());
                
                _objReportResponseModel = await _apiServices.GetReportListAsync(new Get_API_Url().ReportListApi(_baseUrl), true, _objHeaderModel, _objReportRequestModel);
                var Result = _objReportResponseModel.Response;
                if (Result.StatusCode == 200)
                {
                    if (Result.ReportListPatient.Count > 0)
                    {
                        listReportListByFilter.ItemsSource = Result.ReportListPatient;
                        //  DependencyService.Get<IToast>().ShowToast(Result.Message);
                        // await App.NavigationPage.Navigation.PushAsync(new OTPVerificationPage(Result));
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().ShowToast("There is No Data To Display!");
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
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
                await Navigation.PopAllPopupAsync();
                var msg = ex.Message;
            }
            }

        private  void imgFilter_Click(object sender, EventArgs e)
        {
            App.NavigationPage.Navigation.PushAsync(new FilterOptionPage());
             //Navigation.PushAsync(new FilterOptionPage());
        }

        private void imgViewPic_Click(object sender, EventArgs e)
        {
            var obj = ((TappedEventArgs)e).Parameter as ReportListPatient;
            var Url = obj.ReportUrl; 
          //  var ReportUrl = "http://www.orimi.com/pdf-test.pdf";
            var result = CrossShare.Current.CanOpenUrl(Url);
            if (result)
            {
                CrossShare.Current.OpenBrowser(Url);
            }
            else
            {
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
            }
        }
        private void imgDownloadPic_Click(object sender, EventArgs e)
        {
            var obj = ((TappedEventArgs)e).Parameter as ReportListPatient;
              var Url = obj.ReportUrl;
           // var Url = "http://www.orimi.com/pdf-test.pdf";
            DownlodFile(Url);
        }
        private void imgSharePic_Click(object sender, EventArgs e)
        {
            var obj = ((TappedEventArgs)e).Parameter as ReportListPatient;
            var Url = obj.ReportUrl; 
           // var ReportUrl1 = "http://www.orimi.com/pdf-test.pdf";
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = "Click the link" + Url + "to open the Report",
                Title = "Share"
            });
        }
    }
}