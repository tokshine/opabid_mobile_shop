using Shivonet.MobileShop.Core.Controls;
using Shivonet.MobileShop.Core.Effects;
using Shivonet.MobileShop.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core.Behaviors
{
    public class EntryValidationBehavior : Behavior<RoundedEntry>
    {
        private RoundedEntry _associatedObject;

        protected override void OnAttachedTo(RoundedEntry bindable)
        {
            base.OnAttachedTo(bindable);
            // Perform setup       

            _associatedObject = bindable;

            _associatedObject.TextChanged += _associatedObject_TextChanged;
        }

        void _associatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var source = _associatedObject.BindingContext as ValidationBase;
            if (source != null && !string.IsNullOrEmpty(PropertyName))
            {
                var errors = source.GetErrors(PropertyName).Cast<string>();
                if (errors != null && errors.Any())
                {
                    var borderEffect = _associatedObject.Effects.FirstOrDefault(eff => eff is BorderEffect);
                    if (borderEffect == null)
                    {
                        _associatedObject.Effects.Add(new BorderEffect());
                    }

                    if (Device.OS != TargetPlatform.Windows)
                    {
                       _associatedObject.BackgroundColor = Color.LightGray;
                                           
                    }
                }
                else
                {
                    var borderEffect = _associatedObject.Effects.FirstOrDefault(eff => eff is BorderEffect);
                    if (borderEffect != null)
                    {
                        _associatedObject.Effects.Remove(borderEffect);
                    }

                    if (Device.OS != TargetPlatform.Windows)
                    {
                        _associatedObject.BackgroundColor = Color.Default;
                    }
                }
            }
        }

        protected override void OnDetachingFrom(RoundedEntry bindable)
        {
            base.OnDetachingFrom(bindable);
            // Perform clean up

            _associatedObject.TextChanged -= _associatedObject_TextChanged;

            _associatedObject = null;
        }

        public string PropertyName { get; set; }
    }
}
