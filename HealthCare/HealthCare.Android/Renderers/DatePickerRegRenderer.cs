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

[assembly: ExportRenderer(typeof(DatePickerReg),typeof(DatePickerRegRenderer))]
namespace HealthCare.Droid.Renderers
{
   public class DatePickerRegRenderer: DatePickerRenderer
    {
        public DatePickerRegRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
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
                Drawable imgDropDownArrow1 = Resources.GetDrawable(Resource.Drawable.date);
                imgDropDownArrow1.SetBounds(0, 0, 15, 0);
                Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(null, null, imgDropDownArrow1, null);
            }
        }
    }
}