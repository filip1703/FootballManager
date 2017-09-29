
using System.Windows;
using System;
using System.Globalization;

namespace FootballManager {
    public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter> {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
