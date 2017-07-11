using System.Linq;
using System.Collections.Generic;

namespace Killzone
{
    public class Game
    {
        private List<Player> _players;

        public Game(List<Player> players)
        {
            _players = players;       
        }

        public (Team WinnerTeam, int Rounds) Start()
        {
            int rounds = 0;
            while(MoreThanOneTeamAlive())
            {
                var playersAlive = _players.Where(p => p.Hp > 0).ToList();

                foreach (var player in playersAlive)
                {
                    player.Attack(playersAlive.Where(p => p.Team.Id != player.Team.Id).ToList());

                    if (playersAlive.Any(p => p.Hp <= 0))
                        break;
                }                    

                rounds++;
            }

            return (_players.First(p => p.Hp > 0).Team, rounds);
        }

        private bool MoreThanOneTeamAlive()
        {
            var lasPlayerAlive = _players.First(p => p.Hp > 0);
            foreach (var player in _players.Where(p => p.Hp > 0))
            {
                var actualPlayerAlive = player;

                if (player.Hp > 0 && actualPlayerAlive.Team.Id != lasPlayerAlive.Team.Id)
                    return true;
            }           

            return false;
        }
    }
}