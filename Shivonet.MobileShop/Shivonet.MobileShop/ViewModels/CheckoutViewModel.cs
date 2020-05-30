using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.Models;
using Shivonet.MobileShop.Core.ViewModels.Base;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core.ViewModels
{
    public class CheckoutViewModel: ViewModelBase
    {
        private readonly IOrderDataService _orderDataService;
        private readonly ISettingsService _settingsService;
        private Order _order;
       // private Address _address;

        public CheckoutViewModel(IConnectionService connectionService, 
            INavigationService navigationService, IDialogService dialogService, 
            IOrderDataService orderDataService, ISettingsService settingsService) 
            : base(connectionService, navigationService, dialogService)
        {
            _orderDataService = orderDataService;
            _settingsService = settingsService;
        }

        public ICommand PlaceOrderCommand => new Command(OnPlaceOrder);

        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object data)
        {
            Order = new Order { OrderId = Guid.NewGuid().ToString(),Address = new Address(),
                UserId = _settingsService.UserIdSetting ,OrderTotal = (decimal)data};

            //dont i thnk this line is necessary could be replaced by return Task.FromResult(order)
            return base.InitializeAsync(data);
        }

        private async void OnPlaceOrder()
        {
            await _orderDataService.PlaceOrder(Order);
            MessagingCenter.Send(this, "OrderPlaced");
            await _dialogService.ShowDialog("Order placed successfully", "Success", "OK");
            await _navigationService.PopToRootAsync();
        }
    }
}
