using System;
using System.Globalization;
using System.Windows.Data;

namespace FootballManager {
    
        public class CurrencyConverter : BaseValueConverter<CurrencyConverter> {


            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

                NumberFormatInfo f = new NumberFormatInfo();
                f = culture.NumberFormat;

                string ret = null;


                if (value is decimal) {
                    decimal val = (decimal)value;

                    if (val < 1000) {
                        ret = culture.Name != "en-GB" && culture.Name != "en-US" ? val.ToString("#.# " + f.CurrencySymbol) : val.ToString(f.CurrencySymbol + "#.# ");
                    }
                    else if (val >= 1000 && val < 1000000) {

                        ret = culture.Name != "en-GB" && culture.Name != "en-US" ? val.ToString("#,.#K " + f.CurrencySymbol) : val.ToString(f.CurrencySymbol + "#,.#K");
                    }
                    else if (val >= 1000000) {

                        ret = culture.Name != "en-GB" && culture.Name != "en-US" ? val.ToString("#,,.#M " + f.CurrencySymbol) : val.ToString(f.CurrencySymbol + "#,,.#M");
                    }
                }
                return ret;
            }


            public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
                throw new NotImplementedException();
            }
        }
}

