using Shivonet.MobileShop.Core.Annotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shivonet.MobileShop.Core.Models
{
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Address _address = new Address();
        public string OrderId { get; set; }

        public decimal OrderTotal { get; set; }

        public List<Product> Products { get; set; }

        //public Address Address { get; set; }

        public string UserId { get; set; }


        public Address Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
    }
}
