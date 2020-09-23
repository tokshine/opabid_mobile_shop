using Shivonet.MobileShop.Core.Templates;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shivonet.MobileShop.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShoppingCartView : ContentPage
	{
		public ShoppingCartView()
		{
			InitializeComponent ();
           

        }

        //void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var picker = (Picker)sender;
        //    int selectedIndex = picker.SelectedIndex;

        //    if (selectedIndex != -1)
        //    {
        //        //monkeyNameLabel.Text = (string)picker.ItemsSource[selectedIndex];
        //    }
        //}
    }
}