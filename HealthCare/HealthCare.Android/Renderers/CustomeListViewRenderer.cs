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
using HealthCare.CustomeControls;
using HealthCare.Droid.Renderers;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Util;

//[assembly: ExportRenderer(typeof(CustomeListView), typeof(CustomeListViewRenderer))]
namespace HealthCare.Droid.Renderers
{
    //public class CustomeListViewRenderer : ListViewRenderer
    //{
    //    bool isScroll = false;
    //    public CustomeListViewRenderer(Context context)
    //        : base(context)
    //    {

    //    }

    //    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
    //    {
    //        base.OnElementChanged(e);
    //        //if (e.OldElement == null)
    //        //{
    //        //    return;
    //        //    // Control.Background = null;

    //        //}

    //        // SetControlStyle();
    //        // Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
    //    }
    //    //public override bool DispatchTouchEvent(MotionEvent e)
    //    //{
    //    //    if (isScroll)
    //    //    {
    //    //        return base.DispatchTouchEvent(e); ;
    //    //    }
    //    //    else
    //    //    {
    //    //        return false;
    //    //    }
    //    //}
    //        //protected override void OnDetachedFromWindow()
    //        //{
    //        //    if (Element == null)
    //        //        return;

    //        //    base.OnDetachedFromWindow();
    //        //}
    //        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    //        //{
    //        //    base.OnElementPropertyChanged(sender, e);
    //        //  //  Xamarin.Forms.Device.BeginInvokeOnMainThread(base.Dispose);
    //        //}
    //    }
}