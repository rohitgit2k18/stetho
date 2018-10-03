using HealthCare.Helpers;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HomePage), "PROFILE");
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
        }
        public UserProfilePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //App.DetailPage = this;
        }
        protected override void OnAppearing()
        {
          //  lblPIN.Text = "PIN: " + Settings.UserPin.ToString();
            txtFullname.Text = Settings.Name;
            txtEmail.Text = Settings.Email;
            txtLocation.Text = Settings.Address;
            txtgender.Text = Settings.Gender;
            txtPhoneNo.Text = Settings.PhoneNo;
            txtDOB.Text = Settings.DOB;
            imgProfile.Source = Settings.ProfilePicture;
        }

      

        private async void imgEdit_Click(object sender, EventArgs e)
        {
            await App.NavigationPage.Navigation.PushAsync(new EditProfilePage());
        }
    }
}