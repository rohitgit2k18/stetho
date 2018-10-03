using HealthCare.CustomeControls;
using HealthCare.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(BorderLessPicker), typeof(BorderLessPickerRenderer))]
namespace HealthCare.iOS.Renderers
{
   public class BorderLessPickerRenderer:PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
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
                // UIView paddingView = new UIView(new RectangleF(10, 0, 35, 35));


                var imgDropDownArrow = UIImage.FromFile("arrow1.png");

                //  paddingView.Add(UIView.imgDropDownArrow);


                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIImageView(imgDropDownArrow);


            }
        }
    }
}
