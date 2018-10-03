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
using System.Drawing;

[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderLessDatePickerRenderer))]
namespace HealthCare.iOS.Renderers
{
   public class BorderLessDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
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
               
                  
                 var imgDropDownArrow = UIImage.FromFile("cal_1.png");

              //  paddingView.Add(UIView.imgDropDownArrow);
                

                Control.RightViewMode = UITextFieldViewMode.Always;              
                Control.RightView = new UIImageView(imgDropDownArrow);
               
                
            }
        }
    }
}