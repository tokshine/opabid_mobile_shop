using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.Models;
using Shivonet.MobileShop.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Shivonet.MobileShop.Core.Constants;
using Shivonet.MobileShop.Core.Extensions;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ICatalogDataService _catalogDataService;
        private ObservableCollection<Product> _productsOfTheWeek;

        public HomeViewModel(IConnectionService connectionService,
            INavigationService navigationService,
            IDialogService dialogService,
            ICatalogDataService catalogDataService) : base(connectionService, navigationService, dialogService)
        {
            _catalogDataService = catalogDataService;

            ProductsOfTheWeek = new ObservableCollection<Product>();
        }

        public ICommand ProductTappedCommand => new Command<Product>(OnProductTapped);
        public ICommand AddToCartCommand => new Command<Product>(OnAddToCart);

        public ObservableCollection<Product> ProductsOfTheWeek
        {
            get => _productsOfTheWeek;
            set
            {
                _productsOfTheWeek = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            ProductsOfTheWeek = (await _catalogDataService.GetProductsOfTheWeekAsync()).ToObservableCollection();
        }

        private void OnProductTapped(Product selectedProduct)
        {
            _navigationService.NavigateToAsync<ProductDetailViewModel>(selectedProduct);
        }

        private async void OnAddToCart(Product selectedProduct)
        {
            MessagingCenter.Send(this, MessagingConstants.AddProductToBasket, selectedProduct);
            await _dialogService.ShowDialog("Product added to your cart", "Success", "OK");
        }
    }
}