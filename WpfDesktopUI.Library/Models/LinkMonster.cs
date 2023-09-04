namespace WpfDesktopUI.Library.Models
{
    public class LinkMonster : Monster
    {

        public string Atk { get; }

        public byte LinkRating { get; }

        public string[] LinkArrows { get; }

        public override string LevelDisplay { get; }

        public override string AtkDefDisplay { get; }

        public LinkMonster(
               string name,
               string desc,
               string attribute,
               string race,
               string[] type,
               string atk,
               byte linkRating,
               string[] linkArrows) : base(name, desc, attribute, race, type)
        {
            Atk = atk;
            LinkRating = linkRating;
            LinkArrows = linkArrows;
            LevelDisplay = $"Link {LinkRating}";
            AtkDefDisplay = $"ATK : {Atk}";
        }

    }
}
