using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Shivonet.MobileShop.iOS.Effects;

[assembly: ResolutionGroupName("SvValidation")]
[assembly: ExportEffect(typeof(BorderEffect), "BorderEffect")]
namespace Shivonet.MobileShop.iOS.Effects
{
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                Control.Layer.BorderColor = UIColor.Red.CGColor;
                Control.Layer.BorderWidth = 1;
            }
            catch (Exception)
            {
            }
        }

        protected override void OnDetached()
        {
            try
            {
                Control.Layer.BorderWidth = 0;
            }
            catch (Exception)
            {
            }
        }
    }
}