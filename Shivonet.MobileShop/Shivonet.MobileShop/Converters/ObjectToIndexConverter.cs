using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Shivonet.MobileShop.Core.Converters
{
    [ContentProperty("Items")]
    public class ObjectToIndexConverter<T> : IValueConverter
    {
        public IList<T> Items { set; get; }


        public ObjectToIndexConverter() { Items = new List<T>(); }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { if (value == null || !(value is T) || Items == null) return -1; return Items.IndexOf((T)value); }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { int index = (int)value; if (index < 0 || Items == null || index >= Items.Count) return null; return Items[index]; }
    }
}
