using Shivonet.MobileShop.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shivonet.MobileShop.Core.ViewModels
{
   public class CartPageViewModel : BaseViewModel
    {
        string title;
        public CartPageViewModel()
        {
            Title = "Monkey Finder";

        }

        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged();
            }
        }
    }
}
