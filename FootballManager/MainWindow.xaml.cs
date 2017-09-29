using FootballManager.Core;
using System.Windows;



namespace FootballManager {


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();


            this.DataContext = new MainWindowViewModel();

        }

    }
}
