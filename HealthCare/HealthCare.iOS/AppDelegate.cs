using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Syncfusion.SfSchedule.XForms.iOS;
using ImageCircle.Forms.Plugin.iOS;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.IO;

namespace HealthCare.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            SfScheduleRenderer.Init();
            LoadApplication(new App());
           // UIApplication.SharedApplication.StatusBarHidden = true;
            Downloaded();
            return base.FinishedLaunching(app, options);
        }
        public void Downloaded()
        {

            CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>(file => {

                string fileName = (new NSUrl(file.Url, false)).LastPathComponent;

                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);


            });

            //  FileExtension.SetFileURL(CrossDownloadManager.Current.PathNameForDownloadedFile);
        }
    }
}
