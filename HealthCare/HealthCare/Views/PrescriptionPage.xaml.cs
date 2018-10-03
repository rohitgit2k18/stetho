using HealthCare_API.ApiHandler;
using HealthCare_API.Model;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthCare.Helpers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCare.Models;
using Rg.Plugins.Popup.Extensions;
using HealthCare.Interface;
using Plugin.Share;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrescriptionPage : ContentPage
    {
        #region   varibale Declaration
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "PRESCRIPTIONS");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        private string _baseUrl;
        private string _baseurlDeptList;
        private string _baseUrlDocList;
        private RestApi _apiServices;
        private int DeptId;
        private PrescriptionRequestModel _objPrescriptionRequestModel;
        private PrescriptionResponseModel _objPrescriptionResponseModel;
        private HeaderModel _objHeaderModel;
        #endregion
        public PrescriptionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            _baseUrl = Settings.Url + Domain.PrescriptionApiConstant;
            _apiServices = new RestApi();
            _objPrescriptionRequestModel = new PrescriptionRequestModel();
            _objPrescriptionResponseModel = new PrescriptionResponseModel();          
           // App.DetailPage = this;
            LoadPrescription(string.Empty);
        }
        public async void LoadPrescription(string Keyword)
        {
            try
            {                
                _objPrescriptionRequestModel.key = Keyword;
                await Navigation.PushPopupAsync(new LoadingPopPage());
                _objPrescriptionResponseModel = await _apiServices.GetPrescriptionListAsync(new Get_API_Url().PrescriptionListApi(_baseUrl), true, _objHeaderModel, _objPrescriptionRequestModel);
                    var Result = _objPrescriptionResponseModel.Response;
                    if (Result.StatusCode == 200)
                    {
                    if (Result.PrescriptionList.Count > 0)
                    {
                        //var max = Result.PrescriptionList.Max(e => e.TabletList.Count);
                        //  var xyz = Result.PrescriptionList.SelectMany(e => e.TabletList);
                        // var i = 0;
                        foreach (var items in Result.PrescriptionList)
                        {

                            string concatTabName = null;
                            string concatTabTime = null;
                            items.HeightReq = (items.TabletList.Count * 15) + 100;

                            foreach (var value in items.TabletList)
                            {
                                //items.TabName = value.TabletName;
                                //items.tabTime = value.Repetaion;
                                //items.CompleteString += items.AveragePrice + "\n";
                                //  concatResult += + "\n";
                                //concatResult.Spans.Add(new Span { Text = value.TabletName,ForegroundColor=Color.Red, FontAttributes = FontAttributes.Bold });
                                //concatResult.Spans.Add(new Span { Text = value.Repetaion, ForegroundColor = Color.Blue, FontAttributes = FontAttributes.Bold });
                                //s += concatResult;
                                concatTabName += value.TabletName + "\n";
                                concatTabTime += value.Repetaion + "\n";
                            }
                            //  items.CompleteString = s;
                            items.TabName = concatTabName;
                            items.tabTime = concatTabTime;
                            //i++;
                        }

                        //foreach (var itm in Result.PrescriptionList)
                        //{
                        //    itm.HeightReq = max * 15 + 100;
                        //}
                        listprescriptionListByDoc.ItemsSource = Result.PrescriptionList;
                        // DependencyService.Get<IToast>().ShowToast(Result.Message);
                        await Navigation.PopAllPopupAsync();
                    }
                    else
                    {
                        DependencyService.Get<IToast>().ShowToast("No Data to Display!");
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

        private void imgSharePic_Click(object sender, EventArgs e)
        {
            var obj = ((TappedEventArgs)e).Parameter as PrescriptionList;
           // var Url = obj.ReportURL; 
            var ReportUrl1 = "http://www.orimi.com/pdf-test.pdf";
            String concatResult = "";
            foreach (var value in obj.TabletList)
            {
                concatResult += value.TabletName+ " - "+value.Power+" - "+value.Repetaion+"\n";
            }
          //  ("\n'You are receiving your running length calculation done by the Running Length App from Teijin Aramid.'\n\nYour calculation:\n");
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
               
            Text = "This is My Prescription By"+"\n"+"Dr."+ obj.DoctorName+"\n"+obj.HospitalName+"\n"+obj.CreatedOn+"\n\n"+ "℞" + "\n\n"+ concatResult ,
                Title = "Prescription"
            });
        }

        private async void PrescriptionSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopPage());
          //  listprescriptionListByDoc.ItemsSource = null;
            var key = PrescriptionSearchBar.Text.ToLower();
            if (string.IsNullOrEmpty(key))
            {
                LoadPrescription(string.Empty);
            }
            else
            {
                // var result = listData.Where(x => String.Equals(x.FieldID, "FIELDID KNOWN VALUE");
                listprescriptionListByDoc.ItemsSource = _objPrescriptionResponseModel.Response.PrescriptionList.Where(keyword => keyword.DoctorName.ToLower().Contains(key) || keyword.HospitalName.ToLower().Contains(key));
                // LoadPrescription(key);
            }
            await Navigation.PopAllPopupAsync();
        }
    }
}