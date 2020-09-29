using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shivonet.MobileShop.Core.Models
{


    public class Quantity
    {
        public int Id { get; set; }
        public string Name { get; set; }      

        public int SelIndex { get; set; } //dont bind ,this could be useful lookup
    }

    public static class QuantityData
    {
        public static IList<Quantity> QuantityList { get; private set; }

        static QuantityData()
        {
            QuantityList = new List<Quantity>();
            for (int i = 1; i <= 12; i++)
            {

                var q = new Quantity { Id = i, Name = i.ToString() };
                QuantityList.Add(q);
            }

        }
    }

    public class ShoppingCartItem 
        : INotifyPropertyChanged
    {

        //  private Quantity _selectedQuantity;
        private int _quantity;
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


        //public int Quantity
        //{
        //    get
        //    {
        //        return _quantity;
        //    }
        //    set
        //    {
        //        _quantity = value;
        //        OnPropertyChanged();
        //    }
        //}

        private int _selectedIndex { get; set; }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                //_selectedIndex = value;

                if (this._selectedIndex == value)
                {
                    return;
                }
                _selectedIndex = value;
                OnPropertyChanged();
                Quantity = _selectedIndex +1 ;
                Total= Quantity * Product.Price * (1 - DiscountPercent / 100); 
            }
        }
        
        public IList<Quantity> QuantityList { get { return QuantityData.QuantityList; } }

        public decimal DiscountPercent => 15;

       // public decimal Total => Quantity * Product.Price * (1 - DiscountPercent / 100);

        private decimal _total;
        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {

                _total = value;
                OnPropertyChanged();                
            }
        }


    }
}
