using HealthCare.Interface;
using HealthCare.iOS.Dependencies;
using System;
using System.Collections.Generic;
using System.Text;
using ToastIOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSToast))]
namespace HealthCare.iOS.Dependencies
{

  public  class IOSToast : IToast
    {
        public void ShowToast(string message)
        {
            Toast.MakeText(message).Show();
        }
    }
}

