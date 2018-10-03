
using HealthCare.ViewModels;
using HealthCare_API.ApiHandler;
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
using HealthCare.Models;
using HealthCare.Interface;
using Rg.Plugins.Popup.Extensions;
using System.Text.RegularExpressions;
using HealthCare_API.Model;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRegisterUserPage : ContentPage
    {
        
        
        #region variable Declaration
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private bool IsValid { get; set; }
        private string _baseUrl;
        private RestApi _apiServices;
        private GenderViewModel _objGenderViewModel;
        private RegisterUserRequest _objRegisterUserRequest;
        private RegisterUserResponse _objRegisterUserResponse;
        #endregion
        public NewRegisterUserPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
         

            _objGenderViewModel = new GenderViewModel();
       
            RadioGenderCheck.ItemsSource = _objGenderViewModel.GetRadioType();
            _objRegisterUserRequest = new RegisterUserRequest();
            BindingContext = _objRegisterUserRequest;
            _objRegisterUserResponse = new RegisterUserResponse();
            _apiServices = new RestApi();
            _baseUrl = Settings.Url + Domain.RegisterUserApiConstant;
        }

        private async void BackButton_Click(object sender, EventArgs e)
        {
            await App.NavigationPage.Navigation.PopAsync();
        }

        private async void RegisterUserSubmit_Clicked(object sender, EventArgs e)
        {
            _objRegisterUserRequest.DOB = dateOfBirth.Date;
            _objRegisterUserRequest.Gender = (RadioGenderCheck.SelectedItem as GenderModel).Name;
           
            try
            {

                if (
                      (string.IsNullOrEmpty(_objRegisterUserRequest.FirstName)) ||
                      (string.IsNullOrEmpty(_objRegisterUserRequest.LastName)) ||
                      (string.IsNullOrEmpty(_objRegisterUserRequest.Email)) ||
                      (string.IsNullOrEmpty(_objRegisterUserRequest.Password)) ||
                      (string.IsNullOrEmpty(_objRegisterUserRequest.PhoneNo)) ||
                      (string.IsNullOrEmpty(_objRegisterUserRequest.Address)) ||
                      (string.IsNullOrEmpty(_objRegisterUserRequest.PostalCode))
                   )
                {
                    DependencyService.Get<IToast>().ShowToast("Please fill all the field with (*) first!!");
                }
                else
                {
                    var emails = _objRegisterUserRequest.Email.ToLower();
                    _objRegisterUserRequest.Email = emails;
                    if ((_objRegisterUserRequest.PhoneNo).Length < 10 || (_objRegisterUserRequest.PhoneNo).Length > 20)
                    {
                        DependencyService.Get<IToast>().ShowToast("Phone No should be of 10 digits !");
                    }
                    else
                    {
                        if (!IsValid)
                        {
                            DependencyService.Get<IToast>().ShowToast("Please enter valid Email Address!");
                        }
                        else
                        {
                            if ((_objRegisterUserRequest.Password).Length < 8)
                            {
                                DependencyService.Get<IToast>().ShowToast("Password should be 8 or more characters long!");
                            }
                            else
                            {
                              
                                await Navigation.PushPopupAsync(new LoadingPopPage());
                                _objRegisterUserResponse = await _apiServices.RegisterAsync(new Get_API_Url().RegisterUserApi(_baseUrl), false, new HeaderModel(), _objRegisterUserRequest);
                                var Result = _objRegisterUserResponse.Response;
                                if (Result.StatusCode == 200)
                                {
                                    // Settings.PhoneNo = txtEntry;
                                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                                    await App.NavigationPage.Navigation.PushAsync(new MainPage());
                                    await Navigation.PopAllPopupAsync();
                                }
                                else
                                {
                                    DependencyService.Get<IToast>().ShowToast(Result.Message);
                                    await Navigation.PopAllPopupAsync();
                                }
                            }

                        }
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

        //private void GenderPicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var picker = (Picker)sender;
        //    int selectedIndex = picker.SelectedIndex;

        //    if (selectedIndex != -1)
        //    {
        //        var data = picker.Items[picker.SelectedIndex];
        //        var selectedReason = picker.SelectedItem;
        //        _objRegisterUserRequest.Gender = data;
        //        // VisitingReason = selectedReason.ReasonForVisit;
        //    }
        //    else
        //    {
        //        DependencyService.Get<IToast>().ShowToast("please select Gender!");
        //    }
        //}

        private void txtreasonforvisit2_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            // ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            // Settings.IsEmailValid = IsValid;
        }

        private void txtPhoneNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > 15)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, 15);
        }
    }
}