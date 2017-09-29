using System;
using System.Windows.Media;

namespace FootballManager.Core {
    public class PlayerViewModel : BaseViewModel {

        public string Name { get; set; }

        public string Header { get; set; } = "Player";

        public int Nr { get; set; } = 99;
        public Position Position { get; set; }
        public TacticsPosition TacticsPosition { get; set; } = (TacticsPosition)(-1);

        public TacticsPosition PreviousPosition { get; set; }

        public string AbilityOnPositionRGB {

            get {
                if (Enum.GetName(typeof(TacticsPosition), TacticsPosition).EndsWith(Enum.GetName(typeof(Position), Position)))

                    return "33cc33";

                else
                    return "e60000";
            }
        }

        public string ShortName {
            get {

                if (Name?.Length > 12)
                    return Name.Substring(Name.IndexOf(' '));
                else
                    return Name;
            }
        }

        public int Condition { get; set; } = 1;

        public decimal Ability { get; set; }

        public decimal Potential { get; set; }

        public string Nationality { get; set; }


        public int Height { get; set; }

        public int Age {
            get {

                var today = DateTime.Today;

                int age = today.Year - DateOfBirth.Year;

                if (DateOfBirth > today.AddYears(-age)) age--;

                

                return age;
            }
        }

        public DateTime DateOfBirth { get; set; }

        public decimal Value { get; set; }


    }
    public enum TacticsPosition { LN, LŚN, ŚN, PŚN, PN, LS, LŚPO, ŚPO, PŚPO, PS, LP, LŚP, ŚP, PŚP, PP, LWO, LDP, DP, PDP, PWO, LO, LŚO, ŚO, PŚO, PO, GK = 27,
                                  R1 = 30, R2, R3, R4, R5, R6, R7, R8, R9, R10, R11, R12 }

    
}
