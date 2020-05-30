using Shivonet.MobileShop.Core.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shivonet.MobileShop.Core.Models
{
    public class Address :INotifyPropertyChanged
    {
        private string _street;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Street {
            get {
                return _street;
            }
            set {
                _street = value;
                OnPropertyChanged();
            }
        }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
