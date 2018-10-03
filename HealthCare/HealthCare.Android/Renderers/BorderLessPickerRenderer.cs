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
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Content.Res;

[assembly: ExportRenderer(typeof(BorderLessPicker), typeof(BorderLessPickerRenderer))]
namespace HealthCare.Droid.Renderers
{
   public class BorderLessPickerRenderer : PickerRenderer
    {
        public BorderLessPickerRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
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
                Drawable imgDropDownArrow = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.arrow1, null);
                //Resources.GetDrawable(Resource.Drawable.arrow);                
                imgDropDownArrow.SetBounds(0, 0, 15, 0);
                Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, imgDropDownArrow, null);
            }
        }
    }
}