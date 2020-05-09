using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.ViewModels;
using Shivonet.MobileShop.Test.Mocks;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Shivonet.MobileShop.Test.ViewModel
{
    public class ProductCatalogViewModelTest
    {
        public ProductCatalogViewModelTest()
        {

        }

        [Fact]
        public async Task Products_Not_Null_After_InitializeAsync_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCatalogDataService = new MockCatalogDataService();

            var productCatalogViewModel = 
                new ProductCatalogViewModel(
                    mockConnectionService.Object, 
                    mockNavigationService.Object, 
                    mockDialogService.Object, 
                    mockCatalogDataService);

            await productCatalogViewModel.InitializeAsync(null);

            Assert.NotNull(productCatalogViewModel.Products);
        }

        [Fact]
        public async Task Products_All_Get_Loaded_After_InitializeAsync_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCatalogDataService = new MockCatalogDataService();

            var productCatalogViewModel =
                new ProductCatalogViewModel(
                    mockConnectionService.Object,
                    mockNavigationService.Object,
                    mockDialogService.Object,
                    mockCatalogDataService);
            await productCatalogViewModel.InitializeAsync(null);

            Assert.Equal(10, productCatalogViewModel.Products.Count);
        }

        [Fact]
        public void ProductTappedCommand_Not_Null_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCatalogDataService = new MockCatalogDataService();

            var productCatalogViewModel =
                new ProductCatalogViewModel(
                    mockConnectionService.Object,
                    mockNavigationService.Object,
                    mockDialogService.Object,
                    mockCatalogDataService);

            Assert.NotNull(productCatalogViewModel.ProductTappedCommand);
        }
    }
}
