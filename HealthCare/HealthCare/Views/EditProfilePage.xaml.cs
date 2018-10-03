using HealthCare.ViewModels;
using HealthCare_API.ApiHandler;
using HealthCare_API.RequestModel;
using HealthCare_API.ResponseModel;
using System;
using HealthCare.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthCare.Interface;
using Rg.Plugins.Popup.Extensions;
using HealthCare_API.Model;
using HealthCare.Models;
using System.Globalization;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
using PCLStorage;
using System.IO;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Text.RegularExpressions;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        #region variable dec
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        private bool IsValid { get; set; }
        private MediaFile _mediaFile;
        private string _baseUrl;
        private string _baseUrlUpdate;
        private RestApi _apiServices;
        private GenderViewModel _objGenderViewModel;
        private RegisterUserRequest _objRegisterUserRequest;
        private RegisterUserResponse _objRegisterUserResponse;
        private EditUserResquestModel _objEditUserResquestModel;
        private EditUserResponseModel _objEditUserResponseModel;
        private UploadProfileBase64Req _objUploadProfileBase64Req;
        private HeaderModel _objHeaderModel;   
        #endregion
        public EditProfilePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //_objGenderViewModel = new GenderViewModel();
          //  RadioGenderCheck.ItemsSource = _objGenderViewModel.GetRadioType();           
            _objRegisterUserResponse = new RegisterUserResponse();
            _objRegisterUserRequest = new RegisterUserRequest();
            _objEditUserResquestModel = new EditUserResquestModel();
            _objEditUserResponseModel = new EditUserResponseModel();
            _objHeaderModel = new HeaderModel();
            _objHeaderModel.OTPToken = Settings.TokenCode;
            _baseUrl = Settings.Url + Domain.EditUserApiConstant;
            _apiServices = new RestApi();
            _baseUrlUpdate = Settings.Url + Domain.UpdateUserApiConstant;
            LoaduserDetails();
          //  BindingContext = _objEditUserResponseModel.Response;
        }
        private async void BackButton_Click(object sender, EventArgs e)
        {
            await App.NavigationPage.Navigation.PopAsync();
        }
        public async void LoaduserDetails()
        {
            try
            {
                _objEditUserResquestModel.UserId = Settings.Id;
                await Navigation.PushPopupAsync(new LoadingPopPage());
                _objEditUserResponseModel = await _apiServices.EditUserAsync(new Get_API_Url().EditUserApi(_baseUrl), true, _objHeaderModel, _objEditUserResquestModel);
                var Result = _objEditUserResponseModel.Response;
                if (Result.StatusCode == 200)
                {
                   this.BindingContext = _objEditUserResponseModel.Response;
                    if(Result.Gender.Contains("Male"))
                    {
                       GenderPicker.SelectedIndex = 0;
                    }
                    else
                    {
                        GenderPicker.SelectedIndex = 1;
                    }
                   
                  //  DependencyService.Get<IToast>().ShowToast(Result.Message);
                    //await App.NavigationPage.Navigation.PushAsync(new MainPage());
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

        private async void RegisterUserSubmit_Clicked(object sender, EventArgs e)
        {
            var xyz = _objEditUserResponseModel.Response;
            try
      {
            if (Settings.Id != 0)
            {
                int UserId=_objEditUserResquestModel.UserId = Settings.Id;
                    _objRegisterUserRequest.FirstName = _objEditUserResponseModel.Response.FirstName;
                    _objRegisterUserRequest.MiddelName = _objEditUserResponseModel.Response.MiddelName;
                    _objRegisterUserRequest.LastName = _objEditUserResponseModel.Response.LastName;
                    _objRegisterUserRequest.PhoneNo = _objEditUserResponseModel.Response.PhoneNo;
                    _objRegisterUserRequest.Email = _objEditUserResponseModel.Response.Email;
                    _objRegisterUserRequest.Password = _objEditUserResponseModel.Response.Password;
                    _objRegisterUserRequest.Gender = _objEditUserResponseModel.Response.Gender;
                    _objRegisterUserRequest.DOB = _objEditUserResponseModel.Response.DOB;
                    _objRegisterUserRequest.Address = _objEditUserResponseModel.Response.Address;
                    _objRegisterUserRequest.PostalCode = _objEditUserResponseModel.Response.PostalCode;

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
                                    _objRegisterUserResponse = await _apiServices.UpdateUserAsync(new Get_API_Url().UpdateUserApi(_baseUrlUpdate,UserId), true, _objHeaderModel, _objRegisterUserRequest);
                                    var Result = _objRegisterUserResponse.Response;
                                    if (Result.StatusCode == 200)
                                    {
                                        // Settings.PhoneNo = txtEntry;
                                        if (_objRegisterUserRequest.DOB != null)
                                        {
                                            DateTime dts = Convert.ToDateTime(_objRegisterUserRequest.DOB.ToString());
                                            //DateTime date = Convert.ToDateTime(dts, CultureInfo.InvariantCulture);                           
                                            //DateTime dt = DateTime.Parse(dts,  CultureInfo.InvariantCulture);
                                            string dateOfBirth = dts.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
                                            Settings.DOB = dateOfBirth;
                                        }
                                        else
                                        {
                                            Settings.DOB = _objRegisterUserRequest.DOB.ToString();
                                        }
                                        // Settings.UserName = Result.UserName;
                                        Settings.Name = _objRegisterUserRequest.FirstName+" "+ _objRegisterUserRequest.LastName;
                                       // Settings.Id = _objRegisterUserRequest.Id;
                                      //  Settings.ProfilePicture = Result.ProfilePicture;
                                        //"https://media.licdn.com/mpr/mpr/shrinknp_200_200/AAEAAQAAAAAAAAV7AAAAJDZjNjkwZDg4LTEyMjktNGJjYy05ZjIyLWE5M2VhMjJkNDE2ZA.jpg"; //"http://www.pathlab360.com/images/doctor-single.jpg"; //
                                        Settings.Address = _objRegisterUserRequest.Address;
                                        //Settings.RoleId = Result.RoleId;
                                        //Settings.TokenCode = Result.TokenCode;
                                       // Settings.UserPin = Result.UserPin;
                                        Settings.PhoneNo = _objRegisterUserRequest.PhoneNo;
                                        Settings.Gender = _objRegisterUserRequest.Gender;
                                        Settings.Email = _objRegisterUserRequest.Email;
                                       // Settings.IsLoggedIn = true;

                                        DependencyService.Get<IToast>().ShowToast(Result.Message);
                                        await App.NavigationPage.Navigation.PushAsync(new UserProfilePage());
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
            else
            {
                DependencyService.Get<IToast>().ShowToast("Please login again!");
            }
      }
            catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }

        private void GenderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var data = picker.Items[picker.SelectedIndex];
                var selectedReason = picker.SelectedItem;
                _objEditUserResponseModel.Response.Gender = data;
                // VisitingReason = selectedReason.ReasonForVisit;
            }
            else
            {
                DependencyService.Get<IToast>().ShowToast("please select Gender!");
            }
        }
        private void txtPhoneNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 0 && e.NewTextValue.Length > 15)
                ((Entry)sender).Text = e.NewTextValue.Substring(0, 15);
        }
        private void txtreasonforvisit2_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            // ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            // Settings.IsEmailValid = IsValid;
        }
        private async void imgEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //FileData filedata = await CrossFilePicker.Current.PickFile();
                //var fileBytes = filedata.DataArray;
                //var Name = filedata.FileName;
                //IFolder rootFolder = FileSystem.Current.LocalStorage;
                //IFolder folder = await rootFolder.CreateFolderAsync("ProfileFolder", CreationCollisionOption.OpenIfExists);
                //IFile file = await folder.CreateFileAsync("file", CreationCollisionOption.OpenIfExists);

                //using (Stream stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
                //{
                //    await stream.WriteAsync(fileBytes, 0, fileBytes.Length);

                //}
                //_objEditUserResponseModel.Response.ProfilePicture = ImageSource.FromStream(() => new MemoryStream(fileBytes)).ToString();

                // await Navigation.PushPopupAsync(new UploadConfirmation(fileBytes, Name));
                await CrossMedia.Current.Initialize();

                //if (!CrossMedia.Current.IsPickPhotoSupported)
                //{
                //    await DisplayAlert("No PickPhoto", ":( No PickPhoto available.", "OK");
                //    return;
                //}

                //_mediaFile = await CrossMedia.Current.PickPhotoAsync();

                //if (_mediaFile == null)
                //    return;
                //System.Diagnostics.Debug.WriteLine(_mediaFile.Path);
                //// LocalPathLabel.Text = _mediaFile.Path;

                //_objEditUserResponseModel.Response.ProfilePicture = ImageSource.FromStream(() =>
                //{
                //    return _mediaFile.GetStream();
                //}).ToString();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                   await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                _mediaFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    //PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                    //CompressionQuality = 92

                });


                if (_mediaFile == null)
                    return;

                imgProfile.Source = ImageSource.FromStream(() =>
                {
                    // var stream = _mediaFile.GetStream();
                    // _mediaFile.Dispose();
                    return _mediaFile.GetStream();
                });
                _objUploadProfileBase64Req = new UploadProfileBase64Req();
                var ImageName= _mediaFile.Path.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                //var Byt = ReadFully(_mediaFile.GetStream());
                // string base64 = Convert.ToBase64String(Byt);
                // _objUploadProfileBase64Req.ImageName = ImageName;
                // _objUploadProfileBase64Req.ProfilePicture = base64;
                // await Navigation.PushPopupAsync(new UploadConfirmation(_objUploadProfileBase64Req));
                await Navigation.PushPopupAsync(new UploadConfirmation(_mediaFile));
            }
           catch(Exception ex)
            {
                var msg = ex.Message;
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);

                //int read;
                //while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                //{
                //    ms.Write(buffer, 0, read);
                //}
                return ms.ToArray();
            }
        }
    }
}