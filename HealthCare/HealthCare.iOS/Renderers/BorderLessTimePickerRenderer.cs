using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.ComponentModel;
using HealthCare.CustomeControls;
using HealthCare.iOS.Renderers;

[assembly: ExportRenderer(typeof(BorderLessTimePicker), typeof(BorderLessTimePickerRenderer))]
namespace HealthCare.iOS.Renderers
{
  public  class BorderLessTimePickerRenderer:TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            SetControlStyle();
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
            SetControlStyle();
        }
        private void SetControlStyle()
        {
            if (Control != null)
            {
                var imgDropDownArrow = UIImage.FromFile("clock_2.png");
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIImageView(imgDropDownArrow);
            }
        }
    }
}