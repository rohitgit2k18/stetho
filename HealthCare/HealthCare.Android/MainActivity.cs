using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.IO;
using System.Linq;
using AsNum.XFControls.Droid;
using Plugin.Permissions;
using Xamarin.Forms;

namespace HealthCare.Droid
{

    //[Activity(Label = "Mobile App", Theme = "@style/splashscreen", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    //public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity

    [Activity(Label =" "+"Stetho™", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);
         
            base.OnCreate(bundle);         
            global::Xamarin.Forms.Forms.Init(this, bundle);          
            AsNumAssemblyHelper.HoldAssembly();
            ImageCircleRenderer.Init();
            Downloaded();
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            try
            {
                global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

        }

        public void Downloaded()
        {
           try
            {
                CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>(file => {

                    string fileName = Android.Net.Uri.Parse(file.Url).Path.Split('/').Last();
                    return Path.Combine(ApplicationContext.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName);

                });
            }
          catch(Exception ex)
            {
                var msg = ex.Message;
            }

        }
    }
}

