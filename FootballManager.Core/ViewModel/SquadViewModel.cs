using GongSolutions.Wpf.DragDrop;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;

namespace FootballManager.Core {
    public class SquadViewModel : BaseViewModel, IDropTarget {
        
        //all players in a team
        public List<PlayerViewModel> Players { get; set; }

        //all possible positions on the pitch
        public ObservableCollection<PlayerViewModel> TacticsPositions { get; set; } = new ObservableCollection<PlayerViewModel>(new PlayerViewModel[30]);
         
        //positions in specific tactics, 11 players only
        public ICollectionView CurrentSquad { get; set; }

        public ObservableCollection<TacticsPosition> CurrentSquadPositions { get; set; }
        
        public ICommand ModifyCurrentSquadCommand { get; set; }
        public ICommand RemoveFromCurrentSquadCommand { get; set; }

        //indicates whether it is TacticsViewModel created which ownes this SquadViewModel
        public bool IsInTactics { get; set; }
        
        public TacticsPosition[] Formation { get; set; }

        public SquadViewModel() {

            var players = TeamSquadHelper.GetSquad();
            this.Players = players.Select(x => new PlayerViewModel { Ability = x.Ability, DateOfBirth = x.DateOfBirth, Height = x.Height, Name = x.Name, Nationality = x.Nationality, Value = x.Value, Position = x.Position, Potential = x.Potential }).ToList();
            
            CurrentSquad = new CollectionViewSource { Source = TacticsPositions }.View; 
            CurrentSquad.Filter = x => x != null;
            CurrentSquad.SortDescriptions.Add(new SortDescription("TacticsPosition", ListSortDirection.Descending));
            

            Formation = new int[11] { 27, 24, 23, 21, 20, 14, 13, 11, 10, 3, 1 }.Select(x => (TacticsPosition)x).ToArray();
            
            foreach (var x in Formation) {
                TacticsPositions[(int)x] = new PlayerViewModel() { TacticsPosition = x };
                
            }

            CurrentSquadPositions = new ObservableCollection<TacticsPosition>(TacticsPositions.Where(x => x != null).Reverse().Select(x => x.TacticsPosition));
            //not positioned
            CurrentSquadPositions.Add(TacticsPosition.NP);

            ModifyCurrentSquadCommand = new RelayParameterizedCommand(ModifyCurrentSquad);
            RemoveFromCurrentSquadCommand = new RelayParameterizedCommand(RemoveFromCurrentSquad);
        }

        

        private void ModifyCurrentSquad(object parameter) {

            if (parameter is PlayerViewModel) {
                PlayerViewModel p = (PlayerViewModel)parameter;
                
                //if we want to add player or change his position
                if (p.TacticsPosition > TacticsPosition.NP) {

                    //new index of player in tactics
                    int index = (int)p.TacticsPosition;

                    //player in current squad, we want to change his position
                    if (TacticsPositions.Contains(p)) {

                        //new position is not in current tactics, we have to remove previous position
                        if (TacticsPositions[index] == null)
                            RemoveFromCurrentSquad(Tuple.Create(p, RemoveFromSquadOption.RemovePosition));

                        //new position is in current tactics but is busy by another player so we have to swap position between two players
                        else if (TacticsPositions[index].Name != null) {
                            
                            //old index of Player p in tactics
                            int tempIndex = TacticsPositions.IndexOf(p);

                            if (TacticsPositions[index].TacticsPosition != (TacticsPosition)tempIndex) {

                                //we have to move another player to old Player p position, SelectionChanged will be invoked
                                TacticsPositions[index].TacticsPosition = (TacticsPosition)tempIndex;

                                //or we will do it by ourselves if there is no TacticsViewModel which ownes this SquadViewModel
                                if (!this.IsInTactics)
                                    ModifyCurrentSquad(TacticsPositions[index]);
                            }

                        }
                        //new position is free and in current tactics, we have to remove player from his previous position
                        else
                            RemoveFromCurrentSquad(Tuple.Create(p, RemoveFromSquadOption.RemovePlayer));
                           
                    }

                    //player not in current squad, we want to add him
                    else {

                        //player can be add only to one of 11 positions in current tactics
                        if (TacticsPositions[index] == null)
                            return;

                        //there is another player at this position
                        else if (TacticsPositions[index].Name != null) {
                            
                            //we have to remove another player, SelectionChanged will be invoked
                            TacticsPositions[index].TacticsPosition = TacticsPosition.NP;
                            
                        }

                    }

                    //finally we add player to his new position in squad
                    TacticsPositions[index] = p;
                    
                }

                //we want to remove player from squad
                else if (p.TacticsPosition == TacticsPosition.NP) {

                    int index = TacticsPositions.IndexOf(p);
                    
                    //if player is in squad we gonna remove him
                    if (index > -1)
                        RemoveFromCurrentSquad(Tuple.Create(TacticsPositions[index], RemoveFromSquadOption.RemovePlayer));
                }
            }
        }
    

        private void RemoveFromCurrentSquad(object parameter) {

            //method invoked in ModifyCurrentSquad
            if (parameter is Tuple<PlayerViewModel,RemoveFromSquadOption>) {
                Tuple<PlayerViewModel, RemoveFromSquadOption> tuple = (Tuple<PlayerViewModel, RemoveFromSquadOption>)parameter;

                int indexToRemove = TacticsPositions.IndexOf(tuple.Item1);

                if (tuple.Item2.Equals(RemoveFromSquadOption.RemovePlayer)) {
                    //replacing real player by dummy one for proper tactics display
                    TacticsPositions[indexToRemove] = new PlayerViewModel { TacticsPosition = (TacticsPosition)indexToRemove };
                }
                else if (tuple.Item2.Equals(RemoveFromSquadOption.RemovePosition)) {
                    //removing position
                    TacticsPositions[indexToRemove] = null;
                }

            }

            //method invoked by button onclick event in SquadPage.xaml
            else if (parameter is PlayerViewModel) {

                PlayerViewModel p = (PlayerViewModel)parameter;
                
                //removing player
                p.TacticsPosition = TacticsPosition.NP;

                if (!this.IsInTactics)
                    ModifyCurrentSquad(p);
            }
            
        }

        public enum RemoveFromSquadOption { RemovePlayer, RemovePosition }

        public void DragOver(IDropInfo dropInfo) {

            if (dropInfo.Data is PlayerViewModel) {
                
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;

            }
        }

        //drop handler for CurrentSquad buttons in SquadPage.xaml
        public void Drop(IDropInfo dropInfo) {

            PlayerViewModel p = (PlayerViewModel)dropInfo.Data;
            
            //buttons corresponds to players in CurrentSquad
            TacticsPosition position = ((PlayerViewModel)dropInfo.TargetItem).TacticsPosition;
            
            //adding player to position
            p.TacticsPosition = position;
            
            if (!this.IsInTactics)
                ModifyCurrentSquad(p);
            
        }
    }
}
