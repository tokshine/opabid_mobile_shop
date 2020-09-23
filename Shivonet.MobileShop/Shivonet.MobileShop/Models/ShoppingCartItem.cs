using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shivonet.MobileShop.Core.Models
{


    public class Quantity
    {
        public int Id { get; set; }
        public string Name { get; set; }

   

    }

    public static class QuantityData
    {
        public static IList<Quantity> QuantityList { get; private set; }

        static QuantityData()
        {
            QuantityList = new List<Quantity>();
            for (int i = 1; i <= 15; i++)
            {

                var q = new Quantity { Id = i, Name = i.ToString() };
                QuantityList.Add(q);
            }

        }
    }

    public class ShoppingCartItem 
        : INotifyPropertyChanged
    {

        private Quantity _selectedQuantity;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int ShoppingCartItemId { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // public Quantity  SelectedQuantity { get; set; }

        public Quantity SelectedQuantity
        {
            get => _selectedQuantity;
            set
            {
                _selectedQuantity = value;
                OnPropertyChanged();
            }
        }

        public IList<Quantity> QuantityList { get { return QuantityData.QuantityList; } }

        public decimal DiscountPercent => 15;

        public decimal Total => Quantity * Product.Price * (1 - DiscountPercent / 100);
    }
}
