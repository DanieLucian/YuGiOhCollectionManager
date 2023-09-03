using SqliteDataAccess.Library.DTOs;

namespace WpfDesktopUI.Library.Models
{
    public class PendulumMonster : StandardMonster
    {

        public byte Scale { get; }

        public PendulumMonster(PendulumMonsterDTO pendulumMonster) 
            : base(pendulumMonster)
        {
            Scale = pendulumMonster.Scale;
        }

    }
}
