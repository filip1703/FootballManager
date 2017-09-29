using GongSolutions.Wpf.DragDrop;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Input;

namespace FootballManager.Core {
    public class SquadViewModel : BaseViewModel, IDropTarget {

        public List<PlayerViewModel> Players { get; set; }

        public ObservableCollection<PlayerViewModel> TacticsPositions { get; set; } = new ObservableCollection<PlayerViewModel>(new PlayerViewModel[30]);

        public ObservableCollection<PlayerViewModel> CurrentSquad { get; set; } = new ObservableCollection<PlayerViewModel>(new PlayerViewModel[23]);

        public IEnumerable<TacticsPosition> CurrentSquadPositions {

            get {

                return CurrentSquad.Select(x => x.TacticsPosition);
            } }


        public ICommand AddToCurrentSquadCommand { get; set; }
        public ICommand RemoveFromCurrentSquadCommand { get; set; }

        
        public TacticsPosition[] Formation { get; set; }
        public SquadViewModel() {

            var players = TeamSquadHelper.GetSquad();
            this.Players = players.Select(x => new PlayerViewModel { Ability = x.Ability, DateOfBirth = x.DateOfBirth, Height = x.Height, Name = x.Name, Nationality = x.Nationality, Value = x.Value, Position = x.Position, Potential = x.Potential }).ToList();

            

            Formation = new int[11] { 27, 24, 23, 21, 20, 14, 13, 11, 10, 3, 1 }.Select(x => (TacticsPosition)x).ToArray();


            
            int i=0;

            foreach (var x in Formation) {
                TacticsPositions[(int)x] = new PlayerViewModel() { TacticsPosition = x };
                CurrentSquad[i++] = TacticsPositions[(int)x];
            }

            for (int j=30; j<42; j++) {

                CurrentSquad[i++] = new PlayerViewModel() { TacticsPosition = (TacticsPosition)j };
            }


            AddToCurrentSquadCommand = new RelayParameterizedCommand(AddToCurrentSquad);
            RemoveFromCurrentSquadCommand = new RelayParameterizedCommand(RemoveFromCurrentSquad);
        }

        private void AddToCurrentSquad(object parameter) {

            if (parameter is PlayerViewModel) {
                PlayerViewModel p = (PlayerViewModel)parameter;
                if (p.Name != null) {

                    if (CurrentSquad.Contains(p)) {

                        
                        CurrentSquad[CurrentSquad.IndexOf(p)] = new PlayerViewModel() { TacticsPosition = p.PreviousPosition };

                        if (p.PreviousPosition < TacticsPosition.R1)
                            TacticsPositions[(int)p.PreviousPosition] = new PlayerViewModel() { TacticsPosition = p.PreviousPosition };
                    }
                            
                    if (p.TacticsPosition < TacticsPosition.R1)
                        TacticsPositions[(int)p.TacticsPosition] = p;

                    CurrentSquad[CurrentSquad.IndexOf(CurrentSquad.Where(x => x.TacticsPosition == p.TacticsPosition).FirstOrDefault())] = p;

                    p.PreviousPosition = p.TacticsPosition;
                }
            }
        }

        private void RemoveFromCurrentSquad(object parameter) {

            if (parameter is PlayerViewModel) {
                PlayerViewModel p = (PlayerViewModel)parameter;
                if (p.Name != null) {
                    CurrentSquad[CurrentSquad.IndexOf(p)] = new PlayerViewModel() { TacticsPosition = p.TacticsPosition };
                    if (p.TacticsPosition < TacticsPosition.R1)
                        TacticsPositions[TacticsPositions.IndexOf(p)] = new PlayerViewModel() { TacticsPosition = p.TacticsPosition };

                    p.TacticsPosition = (TacticsPosition)(-1);
                    
                }
            }
                
        }

        public void DragOver(IDropInfo dropInfo) {

            if (dropInfo.Data is PlayerViewModel) {



                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;

            }
        }

        public void Drop(IDropInfo dropInfo) {

            PlayerViewModel p = (PlayerViewModel)dropInfo.Data;

            if (CurrentSquad.Contains(p)) {
                TacticsPositions[TacticsPositions.IndexOf(p)] = new PlayerViewModel() { TacticsPosition = p.TacticsPosition };
                CurrentSquad[CurrentSquad.IndexOf(p)] = new PlayerViewModel() { TacticsPosition = p.TacticsPosition };
            }

            TacticsPosition position = ((PlayerViewModel)dropInfo.TargetItem).TacticsPosition;
            p.TacticsPosition = position;

            if (position < TacticsPosition.R1)
                TacticsPositions[(int)position] = p;


            
            CurrentSquad[CurrentSquad.IndexOf(CurrentSquad.Where(x => x.TacticsPosition == position).FirstOrDefault())] = p;
            
            
            
        }
    }
}
