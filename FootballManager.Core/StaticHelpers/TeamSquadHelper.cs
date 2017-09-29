
using System;
using System.Collections.Generic;


namespace FootballManager.Core {
    static class TeamSquadHelper {

        public static List<Player> GetSquad() {


            List<Player> players = new List<Player>();

            players.Add(new FootballManager.Core.Player() { Name = "Giacomo Bonaventura", DateOfBirth = new DateTime(1989, 8, 22), Height = 180, Nationality = "Italy", Position = Position.ŚP, Value = 24000000, Ability = 3.5m, Potential = 3.5m });
            players.Add(new FootballManager.Core.Player() { Name = "Alessio Romagnoli", DateOfBirth = new DateTime(1995, 1, 12), Height = 188, Nationality = "Italy", Position = Position.ŚO, Value = 25000000, Ability = 3.0m, Potential = 4.5m });
            players.Add(new FootballManager.Core.Player() { Name = "Ricardo Rodriguez", DateOfBirth = new DateTime(1992, 8, 25), Height = 180, Nationality = "Switzerland", Position = Position.LO, Value = 17000000, Ability = 3.5m, Potential = 4.0m });
            players.Add(new FootballManager.Core.Player() { Name = "Gianluigi Donnarumma", DateOfBirth = new DateTime(1999, 2, 25), Height = 196, Nationality = "Italy", Position = Position.GK, Value = 25000000, Ability = 2.5m, Potential = 5.0m });
            players.Add(new FootballManager.Core.Player() { Name = "Mateo Musacchio", DateOfBirth = new DateTime(1990, 7, 26), Height = 182, Nationality = "Argentina", Position = Position.ŚO, Value = 18000000, Ability = 3.5m, Potential = 3.5m });
            players.Add(new FootballManager.Core.Player() { Name = "Andrea Conti", DateOfBirth = new DateTime(1994, 3, 2), Height = 184, Nationality = "Italy", Position = Position.PO, Value = 10000000, Ability = 3.0m, Potential = 4.0m });
            players.Add(new FootballManager.Core.Player() { Name = "Riccardo Montolivo", DateOfBirth = new DateTime(1985, 1, 18), Height = 182, Nationality = "Italy", Position = Position.DP, Value = 3500000, Ability = 3.0m, Potential = 3.0m });
            players.Add(new FootballManager.Core.Player() { Name = "Franck Kessie", DateOfBirth = new DateTime(1996, 12, 19), Height = 183, Nationality = "Cote d'Ivoire", Position = Position.DP, Value = 20000000, Ability = 2.5m, Potential = 4.0m });
            players.Add(new FootballManager.Core.Player() { Name = "Andre Silva", DateOfBirth = new DateTime(1995, 11, 6), Height = 184, Nationality = "Portugal", Position = Position.ŚN, Value = 22000000, Ability = 2.5m, Potential = 4.0m });
            players.Add(new FootballManager.Core.Player() { Name = "Suso", DateOfBirth = new DateTime(1993, 11, 19), Height = 177, Nationality = "Spain", Position = Position.PS, Value = 15000000, Ability = 3.5m, Potential = 4.0m });
            players.Add(new FootballManager.Core.Player() { Name = "Hakan Calhanoglu", DateOfBirth = new DateTime(1994, 2, 8), Height = 178, Nationality = "Turkey", Position = Position.ŚPO, Value = 20000000, Ability = 4.0m, Potential = 4.5m });
            players.Add(new FootballManager.Core.Player() { Name = "Marco Storari", DateOfBirth = new DateTime(1977, 1, 7), Height = 187, Nationality = "Italy", Position = Position.GK, Value = 300000, Ability = 2.5m, Potential = 2.5m });

            return players;

        }
    }
}
