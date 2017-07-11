using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Killzone;

namespace tests
{
    public class PlayerTests
    {
        [Fact]
        public void Given_A_Player_When_Attacking_Then_Injury_Selected_Opponent()
        {            
            var opponents = CreateOpponents();
            var sut = CreatePlayer();
            
            sut.Attack(opponents);

            Assert.True(opponents.First().Hp < 100); 
        }

        [Fact]
        public void Given_A_Player_When_Attacking_50_HitPoints_Then_Injury_50_HitPoints_Selected_Opponent()
        {            
            var expectedHp = 50;
            var opponents = new List<Player>{ new NewPlayer().WithAtk(50).WithDef(0).Build() };
            var sut = CreatePlayer();
            
            sut.Attack(opponents);

            Assert.Equal(expectedHp, opponents.First().Hp); 
        }

        [Fact]
        public void Given_A_Player_When_Attacking_50_HitPoints_Then_Injury_50_HitPoints_Minus_Def_Selected_Opponent()
        {            
            var expectedHp = 70;
            var opponents = CreateOpponents();
            var sut = CreatePlayer();
            
            sut.Attack(opponents);

            Assert.Equal(expectedHp, opponents.First().Hp); 
        }

        [Fact]
        public void Given_A_Player_When_Attacking_Then_Select_Opponent_WithLeastHp()
        {            
            var expectedHp = 20;
            var opponents = new List<Player>{new NewPlayer().Build()};
            opponents.Add(new NewPlayer().WithDef(10).Build());

            var sut = new NewPlayer().WithAtk(40).Build();
            
            sut.Attack(opponents);
            sut.Attack(opponents);

            Assert.Equal(expectedHp, opponents.First().Hp); 
        }

        [Fact]
        public void Given_A_Player_When_Injuried_To_Death_Then_ScreamInAgony()
        {            
            var expectedHp = 0;
            var opponents = new List<Player>{new NewPlayer().Build()};
            opponents.Add(new NewPlayer().WithDef(10).Build());

            var sut = new NewPlayer().WithAtk(50).Build();
            
            sut.Attack(opponents);
            sut.Attack(opponents);

            Assert.Equal(expectedHp, opponents.First().Hp); 
        }
  

        [Fact]
        public void Given_A_Game_When_Starting_Game_Then_Only_One_Team_Win_In_10_Rounds()
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

            Assert.Equal("Team 3", result.WinnerTeam.Name); 
            Assert.Equal(7, result.Rounds); 
        }        

        public List<Player> CreateOpponents()
        {
            return new List<Player>{ new Player(20, 20) };
        }

        public Player CreatePlayer()
        {
            return new NewPlayer().WithAtk(50).WithDef(10).Build();
        }
    }

    public class NewPlayer
    {
        public NewPlayer()
        {
            _player = new Player(0, 0);        
        }

        private Player _player;

        public NewPlayer WithAtk(int atk)
        {
            _player = new Player(atk, _player.Def);
            return this;
        }

        public NewPlayer WithDef(int def)
        {
            _player = new Player(_player.Atk, def);
            return this;
        }

        public Player Build()
        {
            return _player;
        }
    }
}