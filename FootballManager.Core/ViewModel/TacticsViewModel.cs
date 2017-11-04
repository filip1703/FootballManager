using GongSolutions.Wpf.DragDrop;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace FootballManager.Core {


    public class TacticsViewModel : BaseViewModel, IDropTarget {

        public string Header { get; set; } = "Overview";


        public ICommand ChangeContentCommand { get; set; }

        public List<BaseViewModel> PageViewModels { get; set; } = new List<BaseViewModel>();

        public BaseViewModel CurrentViewModel { get; set; }



        public SquadViewModel Squad { get; set; }

        public TacticsPiecesViewModel Pieces { get; set; } = new TacticsPiecesViewModel();



        public TacticsViewModel() {


            ChangeContentCommand = new RelayParameterizedCommand(ChangeContent);

            ApplicationViewModel temp = IoC.Get<ApplicationViewModel>();

            SquadViewModel viewModel = (SquadViewModel)temp.ActiveViewModels.Where(x => x.GetType() == typeof(SquadViewModel)).FirstOrDefault();

            if (viewModel != null)
                this.Squad = viewModel;

            else {
                this.Squad = new SquadViewModel();
                temp.ActiveViewModels.Add(this.Squad);
            }

            Squad.IsInTactics = true;

            PageViewModels.Add(this);
            PageViewModels.Add(this.Squad.Players[0]);
            PageViewModels.Add(this.Pieces);

            CurrentViewModel = this;


        }

        private void ChangeContent(object paramerer) {

            if (paramerer is BaseViewModel)

                this.CurrentViewModel = paramerer as BaseViewModel;
        }

        public void DragOver(IDropInfo dropInfo) {

            if (dropInfo.Data is PlayerViewModel) {

                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        //drop handler for tactics board in TacticsPage.xaml
        public void Drop(IDropInfo dropInfo) {


            PlayerViewModel p = (PlayerViewModel)dropInfo.Data;


            double columnWidth = dropInfo.VisualTarget.RenderSize.Width / 5;

            double rowHeight = dropInfo.VisualTarget.RenderSize.Height / 6;

            int colNr = (int)(dropInfo.DropPosition.X / columnWidth);

            int rowNr = (int)(dropInfo.DropPosition.Y / rowHeight);

            int index = colNr + 5 * rowNr;

            //new index in TacticsPositions
            if (index > 24) index = 27;
            
            //if new position not equals previous one
            if (p.TacticsPosition != (TacticsPosition)index) {

                //if new TacticsPosition is not among combobox items in TacticsOverview.xaml
                if (!Squad.CurrentSquadPositions.Contains((TacticsPosition)index)) {

                    //if player is in CurrentSquad (dummy players are always in) we can change his position
                    if (Squad.TacticsPositions.Contains(p) || !Squad.Players.Contains(p))
                        Squad.CurrentSquadPositions[Squad.CurrentSquadPositions.IndexOf(p.TacticsPosition)] = (TacticsPosition)index;

                    p.TacticsPosition = (TacticsPosition)index;
                    //SelectionChanged event will not fire automatically 
                    Squad.ModifyCurrentSquadCommand.Execute(p);
                }

                //dummy players for actual tactics display
                else if (!Squad.Players.Contains(p)) {


                    p.TacticsPosition = (TacticsPosition)index;
                    //dummy players ain't got comboboxes in TacticsOverview.xaml with SelectionChanged event handler
                    Squad.ModifyCurrentSquadCommand.Execute(p);
                }

                //real players who will change position for another in CurrentSquad, SelectionChanged will be invoked automatically
                else
                    p.TacticsPosition = (TacticsPosition)index;

            }


        }
    }
    
}
