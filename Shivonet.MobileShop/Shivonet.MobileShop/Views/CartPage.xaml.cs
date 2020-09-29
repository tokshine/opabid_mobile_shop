using Shivonet.MobileShop.Core.Models;
using Shivonet.MobileShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Shivonet.MobileShop.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();

           var prods=  PopulateData<CartPageViewModel>("ecommerce.json");

            // prods.CartDetails.ForEach(x => x.DiscountPrice = 5);
          
            prods.CartDetails.ForEach(x => x.SelectedIndex  = x.TotalQuantity);
            BindingContext = prods;
                //PopulateData<CartPageViewModel>("ecommerce.json");
        }


        private static T PopulateData<T>(string fileName)
        {
            var file = "Shivonet.MobileShop.Core.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T obj;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }
    }
}