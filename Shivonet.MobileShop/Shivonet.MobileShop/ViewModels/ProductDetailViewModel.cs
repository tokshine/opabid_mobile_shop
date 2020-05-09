using System.Threading.Tasks;
using System.Windows.Input;
using Shivonet.MobileShop.Core.Constants;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.Models;
using Shivonet.MobileShop.Core.ViewModels.Base;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core.ViewModels
{
    public class ProductDetailViewModel: ViewModelBase
    {
        private Product _selectedProduct;

        public ProductDetailViewModel(IConnectionService connectionService, 
            INavigationService navigationService, IDialogService dialogService) 
            : base(connectionService, navigationService, dialogService)
        { }

        public ICommand AddToCartCommand => new Command(OnAddToCart);
        public ICommand ReadDescriptionCommand => new Command(OnReadDescription);

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object product)
        {
            //come back to this
            SelectedProduct = (Product) product;
        }

        private async void OnAddToCart()
        {
            MessagingCenter.Send(this, MessagingConstants.AddProductToBasket, SelectedProduct);
            await _dialogService.ShowDialog("Product added to your cart", "Success", "OK");
        }

        private void OnReadDescription()
        {
            DependencyService.Get<ITextToSpeech>().ReadText(SelectedProduct.LongDescription);
        }
    }
}
