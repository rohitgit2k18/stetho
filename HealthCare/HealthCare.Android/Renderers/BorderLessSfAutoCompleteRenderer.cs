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
using Syncfusion.SfAutoComplete.XForms.Droid;
using HealthCare.CustomeControls;
using HealthCare.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(BorderLessSfAutoComplete), typeof(BorderLessSfAutoCompleteRenderer))]
namespace HealthCare.Droid.Renderers
{
   public class BorderLessSfAutoCompleteRenderer: SfAutoCompleteRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Syncfusion.SfAutoComplete.XForms.SfAutoComplete> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                //Control.Background = null;
                Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
             Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}