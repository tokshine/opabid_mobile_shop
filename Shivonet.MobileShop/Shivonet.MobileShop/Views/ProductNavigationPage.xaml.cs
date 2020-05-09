using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shivonet.MobileShop.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductNavigationPage : NavigationPage
    {
		public ProductNavigationPage ()
		{
			InitializeComponent ();
		}

        public ProductNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}