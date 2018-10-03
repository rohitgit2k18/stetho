using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Xamarin.Forms;
using HealthCare.CustomeControls;
using HealthCare.Droid.Renderers;
using Android.Graphics;

[assembly: ExportRenderer(typeof(BloopyScrollView), typeof(UnScrollableScrollViewRenderers))]
namespace HealthCare.Droid.Renderers
{
  public  class UnScrollableScrollViewRenderers :ScrollViewRenderer
    {
        private Bitmap _bitmapImageBackground;

        protected override async void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            this.VerticalScrollBarEnabled = ((BloopyScrollView)e.NewElement).IsVerticalScrollbarEnabled;
            this.HorizontalScrollBarEnabled = ((BloopyScrollView)e.NewElement).IsHorizontalScrollbarEnabled;

            if (((BloopyScrollView)e.NewElement).IsNativeBouncyEffectEnabled)
            {
                this.OverScrollMode = OverScrollMode.Always;
            }

            //if (((BloopyScrollView)e.NewElement).BackgroundImage != null)
            //{
            //    // retrieving the Bitmap Image from the ImageSource by converting
            //    _bitmapImageBackground = await AndroidImageHelper.GetBitmapFromImageSourceAsync(((BloopyScrollView)e.NewElement).BackgroundImage, this.Context);

            //    // resize the Bitmap to fit the current ScrollView's width and height
            //    var _resizedBitmapImageBackground = new BitmapDrawable(ResizeBitmap(_bitmapImageBackground, this.Width, this.Height));

            //    // Set the background Image
            //    this.Background = _resizedBitmapImageBackground;
            //}
        }

        // Resize the Bitmap
        //private Bitmap ResizeBitmap(Bitmap originalImage, int widthToScae, int heightToScale)
        //{
        //    Bitmap resizedBitmap = Bitmap.CreateBitmap(widthToScae, heightToScale, Bitmap.Config.Argb8888);

        //    float originalWidth = originalImage.Width;
        //    float originalHeight = originalImage.Height;

        //    Canvas canvas = new Canvas(resizedBitmap);

        //    float scale = this.Width / originalWidth;

        //    float xTranslation = 0.0f;
        //    float yTranslation = (this.Height - originalHeight * scale) / 2.0f;

        //    Matrix transformation = new Matrix();
        //    transformation.PostTranslate(xTranslation, yTranslation);
        //    transformation.PreScale(scale, scale);

        //    Paint paint = new Paint();
        //    paint.FilterBitmap = true;

        //    canvas.DrawBitmap(originalImage, transformation, paint);

        //    return resizedBitmap;
        //}
        //public UnScrollableScrollViewRenderers(Context context)
        //   : base(context)
        //{
        //    if (this.Element != null && ChildCount > 0)
        //    {
        //        GetChildAt(0).HorizontalScrollBarEnabled = false;
        //        GetChildAt(0).VerticalScrollBarEnabled = false;
        //    }
        //}



        //protected override void OnElementChanged(VisualElementChangedEventArgs e)
        //{
        //    base.OnElementChanged(e);

        //    if (e.OldElement != null || this.Element == null)
        //    {
        //        return;
        //    }

        //    if (e.OldElement != null)
        //    {
        //        e.OldElement.PropertyChanged -= OnElementPropetyChanged;
        //    }

        //    e.NewElement.PropertyChanged += OnElementPropetyChanged;
        //}

        //private void OnElementPropetyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (this.Element != null && ChildCount > 0)
        //    {
        //        GetChildAt(0).HorizontalScrollBarEnabled = false;
        //        GetChildAt(0).VerticalScrollBarEnabled = false;
        //    }
        //}
    }
}