using RoverController.Lib;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace RoverController.Mobile.Converters
{
    public class StringIsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string str)
            {
                return str.IsEmpty();
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}