using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Syncfusion.SfAutoComplete.XForms.iOS;
using Xamarin.Forms;
using HealthCare.CustomeControls;
using HealthCare.iOS.Renderers;

[assembly: ExportRenderer(typeof(BorderLessSfAutoComplete), typeof(BorderLessSfAutoCompleteRenderer))]
namespace HealthCare.iOS.Renderers
{
  public  class BorderLessSfAutoCompleteRenderer: SfAutoCompleteRenderer
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Syncfusion.SfAutoComplete.XForms.SfAutoComplete> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Layer.BorderWidth = 0;                        
            }
            // Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}