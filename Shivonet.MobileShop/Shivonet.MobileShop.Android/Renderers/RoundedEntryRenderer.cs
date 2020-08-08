using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Shivonet.MobileShop.Core.Controls;
using Shivonet.MobileShop.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRenderer))]
namespace Shivonet.MobileShop.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class RoundedEntryRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (RoundedEntry)Element;

                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background
                    var gradientBackground = new GradientDrawable();
                    gradientBackground.SetShape(ShapeType.Rectangle);
                    gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line
                    gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());
                   
                    // Radius for the curves
                    gradientBackground.SetCornerRadius(DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius))
                        );

                    // set the background of the label
                    Control.SetBackground(gradientBackground);
                }

                // Set padding for the internal text from border
                //Control.SetPadding(
                //    (int)DpToPixels(this.Context, Convert.ToSingle(3)),
                //    15,
                //    (int)DpToPixels(this.Context, Convert.ToSingle(3)),
                //    15);

                Control.SetPadding((int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop, (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                   Control.PaddingBottom);


            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}