using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FootballManager.Core {
    public class MainWindowViewModel : BaseViewModel {
        

        

        public ObservableCollection<MenuLabelViewModel> Labels { get; set; } = new ObservableCollection<MenuLabelViewModel>(MainWindowHelper.GetLabels());

        public ICommand MouseLeftButtonDownCommand { get; set; }
        public ICommand MouseEnterCommand { get; set; }
        public ICommand MouseLeaveCommand { get; set; }


        public MainWindowViewModel() {

            
                
                MouseLeftButtonDownCommand = new RelayParameterizedCommand(LeftButtonDown);
                MouseEnterCommand = new RelayParameterizedCommand(MouseEnter);
                MouseLeaveCommand = new RelayParameterizedCommand(MouseLeave);



        }

        

        private void MouseLeave(object parameter) {

            if (IoC.Get<ApplicationViewModel>().CurrentPage != (ApplicationPage)Enum.GetNames(typeof(ApplicationPage)).Select(x => x.Replace('_', ' ')).ToList().IndexOf((parameter as MenuLabelViewModel).Content))
                (parameter as MenuLabelViewModel).StateRGB = "0d0d0d";
        }

        private void MouseEnter(object parameter) {

            (parameter as MenuLabelViewModel).StateRGB = "a6a6a6";
            

        }

        private void LeftButtonDown(object parameter) {


            MenuLabelViewModel l = Labels.Where(x => x.Content != (parameter as MenuLabelViewModel).Content && x.StateRGB == "a6a6a6").FirstOrDefault();

            if (l != null)
                l.StateRGB = "0d0d0d";

             

            IoC.Get<ApplicationViewModel>().GoToPage(
                (ApplicationPage)Enum.GetNames(typeof(ApplicationPage)).Select(x => x.Replace('_',' ')).ToList().IndexOf((parameter as MenuLabelViewModel).Content));

        }

    }
}

