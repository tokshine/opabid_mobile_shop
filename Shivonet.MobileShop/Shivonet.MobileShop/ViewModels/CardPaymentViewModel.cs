using Shivonet.MobileShop.Core.Contracts.Services.Data;
using Shivonet.MobileShop.Core.Contracts.Services.General;
using Shivonet.MobileShop.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shivonet.MobileShop.Core.ViewModels
{
   public class CardPaymentViewModel : ViewModelBase
    {
        public CardPaymentViewModel(IConnectionService connectionService,
          INavigationService navigationService,
          IDialogService dialogService,
          ICatalogDataService catalogDataService) : base(connectionService, navigationService, dialogService)
        {
          
        }
    }
}
