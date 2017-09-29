using System.Collections.Generic;


namespace FootballManager.Core {
    public static class MainWindowHelper {

        public static List<MenuLabelViewModel> GetLabels() {

            var contents = new List<string>();
            var sepIndexes = new List<int>();
            var items = new List<MenuLabelViewModel>();

            sepIndexes.AddRange(new[] { 0, 1, 6, 10, 12 });

            contents.AddRange(new[] { "Home", "Inbox", "Squad", "Schedule", "Competitions", "Under 20s", "Under 18s", "Tactics", "Team Report", "Staff", "Training",
                                    "Scouting", "Transfers", "Club", "Board", "Finances" });

            for (int i= 0; i<contents.Count; i++) {

                items.Add(new MenuLabelViewModel() {
                    Content = contents[i],
                    IsSeparated = sepIndexes.Contains(i)
                });
            }

            return items; 
            }
        
    }
}
