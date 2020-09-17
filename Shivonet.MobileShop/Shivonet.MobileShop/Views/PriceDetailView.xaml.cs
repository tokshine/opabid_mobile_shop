using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shivonet.MobileShop.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PriceDetailView : ContentView
	{
        public static readonly BindableProperty ActionTextProperty =
          BindableProperty.Create(nameof(ActionText), typeof(string), typeof(PriceDetailView));
        public PriceDetailView ()
		{
			InitializeComponent ();
		}

        public string ActionText
        {
            get { return (string)GetValue(ActionTextProperty); }
            set { this.SetValue(ActionTextProperty, value); }
        }
    }
}