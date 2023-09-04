namespace WpfDesktopUI.Library.Models
{
    public class PendulumMonster : StandardMonster
    {

        public byte Scale { get; }

        public PendulumMonster(
               string name,
               string desc,
               string attribute,
               string race,
               string[] type,
               string atk,
               string def,
               byte lvlRank,
               byte scale) : base(name, desc, attribute, race, type, atk, def, lvlRank)
        {
            Scale = scale;
        }

    }
}
