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
using System.ComponentModel;
using Android.Graphics.Drawables;
using HealthCare.CustomeControls;
using Xamarin.Forms;
using HealthCare.Droid.Renderers;

[assembly: ExportRenderer(typeof(BorderLessTimePicker), typeof(BorderLessTimePickerRenderer))]
namespace HealthCare.Droid.Renderers
{
  public  class BorderLessTimePickerRenderer:TimePickerRenderer
    {
        public BorderLessTimePickerRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
            }
            SetControlStyle();
            // Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            SetControlStyle();
        }
        private void SetControlStyle()
        {
            if (Control != null)
            {
                Drawable imgDropDownArrow = Resources.GetDrawable(Resource.Drawable.clock_2);
                imgDropDownArrow.SetBounds(0, 0, 15, 0);
                Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, imgDropDownArrow, null);
            }
        }
    }
}