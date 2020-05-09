using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.ViewModels;
using Shivonet.MobileShop.Test.Mocks;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Shivonet.MobileShop.Test.ViewModel
{
    public class HomeViewModelTest
    {
        [Fact]
        public async Task ProductsOfTheWeek_Not_Null_After_InitializeAsync_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCatalogDataService = new MockCatalogDataService();

            var homeViewModel = new HomeViewModel(
                mockConnectionService.Object,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockCatalogDataService);

            await homeViewModel.InitializeAsync(null);

            Assert.NotNull(homeViewModel.ProductsOfTheWeek);
        }

        [Fact]
        public async Task ProductsOfTheWeek_All_Get_Loaded_After_InitializeAsync_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCatalogDataService = new MockCatalogDataService();

            var homeViewModel = new HomeViewModel(
                 mockConnectionService.Object,
                 mockNavigationService.Object,
                 mockDialogService.Object,
                 mockCatalogDataService);
            await homeViewModel.InitializeAsync(null);

            Assert.Equal(3, homeViewModel.ProductsOfTheWeek.Count);
        }

        [Fact]
        public void AddToCartCommand_Not_Null_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCatalogDataService = new MockCatalogDataService();

            var homeViewModel = new HomeViewModel(
                 mockConnectionService.Object,
                 mockNavigationService.Object,
                 mockDialogService.Object,
                 mockCatalogDataService);
            Assert.NotNull(homeViewModel.AddToCartCommand);
        }

        [Fact]
        public void ProductTappedCommand_Not_Null_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCatalogDataService = new MockCatalogDataService();

            var homeViewModel = new HomeViewModel(mockConnectionService.Object, mockNavigationService.Object, mockDialogService.Object, mockCatalogDataService);

            Assert.NotNull(homeViewModel.ProductTappedCommand);
        }
    }
}
