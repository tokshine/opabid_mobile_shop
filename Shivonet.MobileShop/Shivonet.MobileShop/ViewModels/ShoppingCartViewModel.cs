using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Shivonet.MobileShop.Core.Constants;
using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.Extensions;
using Shivonet.MobileShop.Core.Models;
using Shivonet.MobileShop.Core.Validation;
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
                //this section  is  triggered by ShoppingCartItems = shoppingCart.ShoppingCartItems.ToObservableCollection();
                _shoppingCartItems = value;
                RecalculateBasket();
                OnPropertyChanged();
               
            }
        }

        //got this style from Essential sf
        private Command removeCommand;

        private Command pickItemCommand;
        public Command RemoveCommand
        {
            get { return this.removeCommand ?? (this.removeCommand = new Command(this.RemoveClicked)); }
        }

       // public ICommand PickItemCommand => new Command(OnPickItemSelectedIndexChanged);

        public Command PickItemCommand
        {
            get { return this.pickItemCommand ?? (this.pickItemCommand = new Command(this.OnPickItemSelectedIndexChanged)); }
        }

        //in case a parameter is required
        //private void OnPickItemSelectedIndexChanged(object obj)
        //{
        //    RecalculateBasket();
        //}

        private void OnPickItemSelectedIndexChanged()
        {
            RecalculateBasket();
        }


        private void RemoveClicked(object obj)
        {
            if (obj is ShoppingCartItem shoppingCartItem)
            {
                this.ShoppingCartItems.Remove(shoppingCartItem);
                RecalculateBasket();
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


        public decimal OrderTotal
        {
            get => _orderTotal;
            set
            {
                _orderTotal = value;
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
            OrderTotal = _orderTotal;
           
        }

        private decimal CalculateOrderTotal()
        {
            decimal total = 0;

            foreach (var shoppingCartItem in ShoppingCartItems)
            {
                decimal discount = (1 - 15M / 100);
                var amount = shoppingCartItem.Quantity * shoppingCartItem.Product.Price * discount;
                total += amount;               
            }

            return total;
        }

        public override async Task InitializeAsync(object data)
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCart(_settingsService.UserIdSetting);
            // ShoppingCartItems = shoppingCart.ShoppingCartItems.ToObservableCollection();
            ShoppingCartItems.Clear();
            UpdateBasket(shoppingCart);
        }

        private void UpdateBasket(ShoppingCart shoppingCart )
        {
            var items = shoppingCart.ShoppingCartItems;
            var products = items.Select(x => x.ProductId).Distinct();
            foreach (var p in products)
            {
                var cartItemForThisProduct = items.First(x => x.ProductId == p); //to avoid the duplicates in my db

                var cartItemsForThisProduct = items.Count(x => x.ProductId == p);

                cartItemForThisProduct.Quantity = cartItemsForThisProduct;

               
                cartItemForThisProduct.Total = cartItemsForThisProduct * cartItemForThisProduct.Product.Price * (1 - cartItemForThisProduct.DiscountPercent / 100);

                // cartItemForThisProduct.SelectedQuantity = new Quantity { Id=cartItemsForThisProduct, Name = cartItemsForThisProduct.ToString()};
                cartItemForThisProduct.SelectedIndex = cartItemsForThisProduct - 1;
                ShoppingCartItems.Add(cartItemForThisProduct);
            }
            ShoppingCartItems = ShoppingCartItems.ToObservableCollection();// this calls the set on ShoppingCartItems ,it is expensive??
            //Should I just use the 
        }

        private async void OnAddProductToBasketReceived(Product product)
        {
            var shoppingCartItem = new ShoppingCartItem() { Product = product, ProductId = product.ProductId, Quantity = 1 };

            await _shoppingCartService.AddShoppingCartItem(shoppingCartItem, _settingsService.UserIdSetting);

            var shoppingCart = await _shoppingCartService.GetShoppingCart(_settingsService.UserIdSetting);
            // ShoppingCartItems = shoppingCart.ShoppingCartItems.ToObservableCollection();
            ShoppingCartItems.Clear();

            // ShoppingCartItems.Add(shoppingCartItem);
            UpdateBasket(shoppingCart);

            //RecalculateBasket();
        }
    }
}

