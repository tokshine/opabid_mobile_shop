using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Shivonet.MobileShop.Core.Controls;
using Shivonet.MobileShop.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ResolutionGroupName("SvValidation")]
[assembly: ExportEffect(typeof(BorderEffect), "BorderEffect")]
namespace Shivonet.MobileShop.Droid.Effects
{
    public class BorderEffect : PlatformEffect
    {
        RoundedEntry _roundedEntry;
        protected override void OnAttached()
        {
            try
            {
                var drawable = ContextCompat.GetDrawable(Control.Context, Resource.Drawable.border);
                Control.SetBackground(drawable);
                //get settings from border.xml
                //not really perfect ,something is redrawing the control to become a rectangle

            }
            catch (Exception)
            {
            }
        }

        protected override void OnDetached()
        {
            try
            {
                Control.SetBackground(null);
            }
            catch (Exception)
            {
            }
        }
    }
}