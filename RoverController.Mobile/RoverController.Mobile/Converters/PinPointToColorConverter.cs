using RoverController.Mobile.Misc;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace RoverController.Mobile.Converters
{
    public class PinPointToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Color.Transparent;

            if (value is PinPointType type)
            {
                switch (type)
                {
                    case PinPointType.Start:
                        color = Color.Blue;
                        break;

                    case PinPointType.Finish:
                        color = Color.Green;
                        break;

                    default:
                        color = (Color)Application.Current.Resources["textColor"];
                        break;
                }
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}