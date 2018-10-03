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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using HealthCare.CustomeControls;
using HealthCare.Droid.Renderers;

[assembly: ExportRenderer(typeof(TextBoxWithIcons), typeof(TextBoxWithIconsRenderer))]
namespace HealthCare.Droid.Renderers
{
  public  class TextBoxWithIconsRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
              //  Control = Android.Graphics.Color.Red;
              //  Control.ba
            }
            // Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}