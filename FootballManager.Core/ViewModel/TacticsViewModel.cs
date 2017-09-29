using GongSolutions.Wpf.DragDrop;
using System.Collections.Generic;



using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;

namespace FootballManager.Core {

    
    public class TacticsViewModel : BaseViewModel, IDropTarget {

        public string Header { get; set; } = "Overview";
        
        //public List<PlayerViewModel> Players { get; set; }

        public ICommand ChangeContentCommand { get; set; }

        public List<BaseViewModel> PageViewModels { get; set; } = new List<BaseViewModel>();

        public BaseViewModel CurrentViewModel { get; set; }

        //public ObservableCollection<PlayerViewModel> Squad.TacticsPositions { get; set; } = new ObservableCollection<PlayerViewModel>(new PlayerViewModel[30]);

        public SquadViewModel Squad { get; set; } 

        public TacticsPiecesViewModel Pieces { get; set; } = new TacticsPiecesViewModel();
            
        

        public TacticsViewModel() {

            //var players = TeamSquadHelper.GetSquad();
            //this.Players = players.Select(x => new PlayerViewModel { Ability = x.Ability, DateOfBirth = x.DateOfBirth, Height = x.Height, Name = x.Name, Nationality = x.Nationality, Value = x.Value, Position = x.Position, Potential = x.Potential }).ToList();
            //Squad.TacticsPositions[10] = Players[2];

            

            ChangeContentCommand = new RelayParameterizedCommand(ChangeContent);

            ApplicationViewModel temp = IoC.Get<ApplicationViewModel>();

            SquadViewModel viewModel = (SquadViewModel)temp.ActiveViewModels.Where(x => x.GetType() == typeof(SquadViewModel)).FirstOrDefault();

            if (viewModel != null)
                this.Squad = viewModel;

            else {
                this.Squad = new SquadViewModel();
                temp.ActiveViewModels.Add(this.Squad);
            }


            
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

        public void Drop(IDropInfo dropInfo) {


            PlayerViewModel p = (PlayerViewModel)dropInfo.Data;


            double columnWidth = dropInfo.VisualTarget.RenderSize.Width / 5;

            double rowHeight = dropInfo.VisualTarget.RenderSize.Height / 6;

            int colNr = (int)(dropInfo.DropPosition.X / columnWidth);

            int rowNr = (int)(dropInfo.DropPosition.Y / rowHeight);

            int index = colNr + 5 * rowNr;

            if (index > 24) index = 27;


            if (dropInfo.VisualTarget.Equals(dropInfo.DragInfo.VisualSource)) {

                if (Squad.TacticsPositions[index]?.Name != null) {

                    PlayerViewModel tempP = Squad.TacticsPositions[index];

                    int tempIndex = Squad.TacticsPositions.IndexOf(p);
                    tempP.TacticsPosition = Squad.TacticsPositions[tempIndex].TacticsPosition;
                    Squad.TacticsPositions[tempIndex] = tempP;
                    
                }
                else {

                    if (Squad.TacticsPositions[index] == null)
                        Squad.TacticsPositions[Squad.TacticsPositions.IndexOf(p)] = null;

                    //else
                    //    Squad.TacticsPositions[Squad.TacticsPositions.IndexOf(p)] = new PlayerViewModel() { TacticsPosition = p.TacticsPosition };
                    
                }
                
            }
            else {

                if (Squad.TacticsPositions.Contains(p)) {

                    if (Squad.TacticsPositions[index] == null)
                        Squad.TacticsPositions[Squad.TacticsPositions.IndexOf(p)] = null;

                    //else
                    //    Squad.TacticsPositions[Squad.TacticsPositions.IndexOf(p)] = new PlayerViewModel() { TacticsPosition = p.TacticsPosition };
                    
                }
                else {

                    if (Squad.TacticsPositions.Where(x => x != null && x.TacticsPosition != (TacticsPosition)index).Count() > 10) {

                        if (Squad.TacticsPositions[index] == null) {
                            
                            return;
                        }
                    }
                }
            }

            p.TacticsPosition = (TacticsPosition)index;
            Squad.TacticsPositions[index] = p;


            int j = 0;

            for (int i = Squad.TacticsPositions.Count - 2; i > 0; i--) {

                if (Squad.TacticsPositions[i - 1] != null)
                    Squad.CurrentSquad[j++] = Squad.TacticsPositions[i - 1];


            }


        }
    }
}
