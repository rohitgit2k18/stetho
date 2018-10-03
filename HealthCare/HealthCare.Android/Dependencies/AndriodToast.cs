using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Interface;
using Plugin.CurrentActivity;
using Xamarin.Forms;
[assembly: Dependency(typeof(HealthCare.Droid.Dependencies.AndriodToast))]
namespace HealthCare.Droid.Dependencies
{
  public class AndriodToast: IToast
    {
        public void ShowToast(string message)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Toast.MakeText(Forms.Context, message, ToastLength.Short).Show();
        }
    }
}