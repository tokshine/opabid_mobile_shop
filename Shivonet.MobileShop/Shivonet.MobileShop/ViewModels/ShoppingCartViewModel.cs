using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Shivonet.MobileShop.Core.Constants;
using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.Extensions;
using Shivonet.MobileShop.Core.Models;
using Shivonet.MobileShop.Core.ViewModels.Base;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        private ObservableCollection<ShoppingCartItem> _shoppingCartItems;
        private readonly ISettingsService _settingsService;
        private readonly IShoppingCartDataService _shoppingCartService;

        private decimal _orderTotal;
        private decimal _taxes;
        private decimal _shipping;
        private decimal _grandTotal;

        public ShoppingCartViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            IShoppingCartDataService shoppingCartService, ISettingsService settingsService) : base(connectionService, navigationService, dialogService)
        {
            _shoppingCartService = shoppingCartService;
            _settingsService = settingsService;
            _shoppingCartItems = new ObservableCollection<ShoppingCartItem>();
            _orderTotal = 0;
        }

        public ICommand CheckOutCommand =>  new Command(OnCheckOut);

        public ObservableCollection<ShoppingCartItem> ShoppingCartItems
        {
            get => _shoppingCartItems;
            set
            {
                _shoppingCartItems = value;
                RecalculateBasket();
                OnPropertyChanged();
            }
        }

        public decimal GrandTotal
        {
            get => _grandTotal;
            set
            {
                _grandTotal = value;
                OnPropertyChanged();
            }
        }

        public decimal Shipping
        {
            get => _shipping;
            set
            {
                _shipping = value;
                OnPropertyChanged();
            }
        }

        public decimal Taxes
        {
            get => _taxes;
            set
            {
                _taxes = value;
                OnPropertyChanged();
            }
        }

        public void InitializeMessenger()
        {
            MessagingCenter.Subscribe<ProductDetailViewModel, Product>(this, MessagingConstants.AddProductToBasket,
                (productDetailViewModel, product) => OnAddProductToBasketReceived(product));
            MessagingCenter.Subscribe<HomeViewModel, Product>(this, MessagingConstants.AddProductToBasket,
                (homeViewModel, product) => OnAddProductToBasketReceived(product));
            MessagingCenter.Subscribe<CheckoutViewModel>(this, "OrderPlaced", model => OnOrderPlaced());
        }

        private void OnCheckOut()
        {
            _navigationService.NavigateToAsync<CheckoutViewModel>(GrandTotal);
        }

        private void OnOrderPlaced()
        {
            ShoppingCartItems.Clear();
        }

        private void RecalculateBasket()
        {
            _orderTotal = CalculateOrderTotal();
            Taxes = _orderTotal * (decimal)0.2;
            Shipping = _orderTotal * (decimal)0.1;
            GrandTotal = _orderTotal + _shipping + _taxes;
        }

        private decimal CalculateOrderTotal()
        {
            decimal total = 0;

            foreach (var shoppingCartItem in ShoppingCartItems)
            {
                total += shoppingCartItem.Quantity * shoppingCartItem.Product.Price;
            }

            return total;
        }

        public override async Task InitializeAsync(object data)
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCart(_settingsService.UserIdSetting);
            ShoppingCartItems = shoppingCart.ShoppingCartItems.ToObservableCollection();
        }

        private async void OnAddProductToBasketReceived(Product product)
        {
            var shoppingCartItem = new ShoppingCartItem() { Product = product, ProductId = product.ProductId, Quantity = 1 };

            await _shoppingCartService.AddShoppingCartItem(shoppingCartItem, _settingsService.UserIdSetting);

            ShoppingCartItems.Add(shoppingCartItem);

            RecalculateBasket();
        }
    }
}
