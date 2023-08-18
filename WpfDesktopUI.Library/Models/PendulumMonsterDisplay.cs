using SqliteDataAccess.Library.Models;

namespace WpfDesktopUI.Library.Models
{
    public class PendulumMonsterDisplay : StandardMonsterDisplay
    {

        public byte Scale { get; }

        public PendulumMonsterDisplay(PendulumMonsterModel pendulumMonster) 
            : base(pendulumMonster)
        {
            Scale = pendulumMonster.Scale;
        }

    }
}
