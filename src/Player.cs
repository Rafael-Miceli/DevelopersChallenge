using System;
using System.Linq;
using System.Collections.Generic;

namespace Killzone
{
    public class Player
    {
        public Player(int atk, int def, Team team = null, string name = null)
        {
            Hp = 100;      
            Atk = atk;      
            Def = def;
            Team = team;
            Name = name;
        }



        public string Name { get; set; }
        public int Atk { get; private set; }
        public int Def { get; private set; }
        public int Hp { get; private set; }
        public Team Team {get; private set; }


        public void Attack(List<Player> opponents)
        {
            var opponentToAtack = ChooseOpponent(opponents);

            opponentToAtack.Injury(Atk);
        }

        private Player ChooseOpponent(List<Player> opponents)
        {   
            var weaker = opponents.Min(o => o.Hp);
            return opponents.FirstOrDefault(o => o.Hp == weaker);
        }

        public void Injury(int damage)
        {
            Hp -= (damage - Def);

            if (Hp <= 0)
                ScreamInAgony();       
        }

        private void ScreamInAgony()
        {
            Console.WriteLine("Dead " + Name);
        }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}