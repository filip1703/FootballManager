using FootballManager.Core;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;

namespace FootballManager {
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            
            switch ((ApplicationPage)value) {

                case ApplicationPage.Squad:
                    return new SquadPage();

                case ApplicationPage.Tactics:
                    return new TacticsPage();

                default:
                    //Debugger.Break();
                    return new Page();
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    
}
