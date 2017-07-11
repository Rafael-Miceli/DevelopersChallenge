using System;
using System.Linq;
using System.Collections.Generic;

namespace Killzone
{
    class Program
    {
        static void Main(string[] args)
        {
            var teams = new List<Team>{
                new Team {
                    Id = 1, 
                    Name = "Team 1"    
                },
                new Team {
                    Id = 2, 
                    Name = "Team 2"    
                },
                new Team {
                    Id = 3, 
                    Name = "Team 3"    
                }
            };

            var players = new List<Player> {
                new Player(50, 10, teams.First(), "Kenny"),
                new Player(50, 40, teams.First(), "Cartman"),
                new Player(50, 40, teams.Skip(1).First(), "Stan"),
                new Player(50, 0, teams.Skip(1).First(), "Kyle"),
                new Player(50, 40, teams.Skip(2).First(), "Butters"),
                new Player(50, 0, teams.Skip(2).First(), "Jimmy")
            };

            var sut = new Game(players);

            var result = sut.Start();
        }
    }
}
