using System;


namespace FootballManager.Core {
    public class Player {

        public string Name { get; set; }
        public Position Position { get; set; }

        public string Nationality { get; set; }


        public int Height { get; set; }

        public decimal Ability { get; set; }

        public decimal Potential { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Value { get; set; }
    }




    public enum Position { LN, PN, ŚN, LS, ŚPO, PS, LP, ŚP, PP, LWO, DP, PWO, LO, ŚO, PO, GK }
}
