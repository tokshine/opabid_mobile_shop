using Shivonet.MobileShop.Core.Bootstrap;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.ViewModels;
using Shivonet.MobileShop.Core.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            InitializeComponent();


           this.MainPage = new NavigationPage(new CartPage());
          // InitializeApp();

           // InitializeNavigation();
        }

        private async Task InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();

            var shoppingCartViewModel = AppContainer.Resolve<ShoppingCartViewModel>();
            shoppingCartViewModel.InitializeMessenger();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
