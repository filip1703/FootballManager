using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FootballManager {
    public class IsNullToBrushConverter : BaseValueConverter<IsNullToBrushConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            return (string)value != null ? Brushes.Blue : Brushes.White;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
