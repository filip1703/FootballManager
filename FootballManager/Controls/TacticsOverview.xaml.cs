using FootballManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootballManager {
    /// <summary>
    /// Interaction logic for TacticsSquad.xaml
    /// </summary>
    public partial class TacticsOverview : UserControl {
        public TacticsOverview() {

            InitializeComponent();
        }

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        //    ComboBox cb = sender as ComboBox;
        //    PlayerViewModel p = (PlayerViewModel)cb.DataContext;
        //    PlayerViewModel pTemplate = (PlayerViewModel)cb.SelectedItem;
        //    p.TacticsPosition = pTemplate.TacticsPosition;
        //    TacticsViewModel viewModel = (TacticsViewModel)this.DataContext;
        //    viewModel.Squad.CurrentSquad[viewModel.Squad.CurrentSquad.IndexOf(pTemplate)] = p;

        //}
    }
}
