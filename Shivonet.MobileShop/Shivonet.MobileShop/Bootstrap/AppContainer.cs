using System;
using Autofac;
using Shivonet.MobileShop.Core.Contracts.Repository;
using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.Repository;
using Shivonet.MobileShop.Core.Services.Data;
using Shivonet.MobileShop.Core.Services.General;
using Shivonet.MobileShop.Core.ViewModels;

namespace Shivonet.MobileShop.Core.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //ViewModels
            builder.RegisterType<CheckoutViewModel>();
            builder.RegisterType<ContactViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<ProductCatalogViewModel>();
            builder.RegisterType<ProductDetailViewModel>();
            builder.RegisterType<RegistrationViewModel>();
            builder.RegisterType<ShoppingCartViewModel>().SingleInstance();
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<CardPaymentViewModel>();

            //services - data
            builder.RegisterType<CatalogDataService>().As<ICatalogDataService>();
            builder.RegisterType<ShoppingCartDataService>().As<IShoppingCartDataService>();
            builder.RegisterType<ContactDataService>().As<IContactDataService>();
            builder.RegisterType<OrderDataService>().As<IOrderDataService>();

            //services - general
            builder.RegisterType<ConnectionService>().As<IConnectionService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<PhoneService>().As<IPhoneService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
