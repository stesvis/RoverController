using System;
using System.Globalization;
using Xamarin.Forms;

namespace RoverController.Mobile.Converters
{
    public class LowerThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = value.ToString();

            if (double.TryParse(number, out double myValue) && double.TryParse(parameter.ToString(), out double myParameter))
            {
                if (myValue < myParameter)
                {
                    return true;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}