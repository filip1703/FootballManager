
using PropertyChanged;
using System.ComponentModel;


namespace FootballManager.Core {

    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
