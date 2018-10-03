﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using HealthCare.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCare.Interface;
using HealthCare_API.ApiHandler;
using HealthCare_API.ResponseModel;
using HealthCare_API.RequestModel;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare_API.Model;

namespace HealthCare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterReportsPopPage: PopupPage
    {
        #region   varibale Declaration

        private string _baseUrl;
        private RestApi _apiServices;
        private PINUpdateRequestModel _objPINUpdateRequestModel;
        private PINUpdateResponseModel _objPINUpdateResponseModel;
        private ReportRequestModel _objReportRequestModel;
        private HeaderModel _objHeaderModel;
        #endregion
        public FilterReportsPopPage()
        {
            InitializeComponent();
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.UpdatePINApiConstant;
            _objPINUpdateRequestModel = new PINUpdateRequestModel();
            _objPINUpdateResponseModel = new PINUpdateResponseModel();
            _objReportRequestModel = new ReportRequestModel();
            _objHeaderModel = new HeaderModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;

            //CloseImage.Rotation = 30;
            //   CloseImage.Scale = 0.3;
            //   CloseImage.Opacity = 0;

            //LoginButton.Scale = 0.3;
            //LoginButton.Opacity = 0;
            //LoginButton1.Scale = 0.3;
            //LoginButton1.Opacity = 0;
            //UsernameEntry.TranslationX = PasswordEntry.TranslationX = -10;
            //UsernameEntry.Opacity = PasswordEntry.Opacity = 0;
        }

        //protected async override Task OnAppearingAnimationEnd()
        //{
        //    var translateLength = 400u;

        //    //await Task.WhenAll(
        //    //    UsernameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
        //    //    UsernameEntry.FadeTo(1),
        //    //    (new Func<Task>(async () =>
        //    //    {
        //    //        await Task.Delay(200);
        //    //        await Task.WhenAll(
        //    //            PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
        //    //            PasswordEntry.FadeTo(1));

        //    //    }))());

        //    //await Task.WhenAll(
        //    //    CloseImage.FadeTo(1),
        //    //    CloseImage.ScaleTo(1, easing: Easing.SpringOut),
        //    //    CloseImage.RotateTo(0),
        //    //    LoginButton.ScaleTo(1),
        //    //    LoginButton.FadeTo(1));
        //}

        //protected async override Task OnDisappearingAnimationBegin()
        //{
        //    //var taskSource = new TaskCompletionSource<bool>();

        //    //var currentHeight = FrameContainer.Height;

        //    ////await Task.WhenAll(
        //    ////    UsernameEntry.FadeTo(0),
        //    ////    PasswordEntry.FadeTo(0),
        //    ////    LoginButton.FadeTo(0));

        //    //FrameContainer.Animate("HideAnimation", d =>
        //    //{
        //    //    FrameContainer.HeightRequest = d;
        //    //},
        //    //start: currentHeight,
        //    //end: 170,
        //    //finished: async (d, b) =>
        //    //{
        //    //    await Task.Delay(300);
        //    //    taskSource.TrySetResult(true);
        //    //});

        //    //await taskSource.Task;
        //}

        //private async void OnLogin(object sender, EventArgs e)
        //{
        //    var loadingPage = new LoadingPopupPage();
        //    await Navigation.PushPopupAsync(loadingPage);
        //    await Task.Delay(2000);
        //    await Navigation.RemovePopupPageAsync(loadingPage);
        //    await Navigation.PushPopupAsync(new LoginSuccessPopupPage());
        //}

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {     
                         
                       // await App.NavigationPage.Navigation.PushAsync(new ReportsPage());
                          await Navigation.PopAllPopupAsync();

            }
            catch (Exception ex)
            {
                DependencyService.Get<IToast>().ShowToast("Something Went Wrong please try Again or check your Internet Connection!!");
                await Navigation.PopAllPopupAsync();
            }
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAllPopupAsync();

        }
    }
}