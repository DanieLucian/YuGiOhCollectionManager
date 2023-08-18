using SqliteDataAccess.Library.Models;

namespace WpfDesktopUI.Library.Models
{
    public class LinkMonsterDisplay : MonsterDisplay
    {
        public string Atk { get; }

        public byte LinkRating { get; }

        public string[] LinkArrows { get; }

        public override string LevelDisplay { get; }

        public override string AtkDefDisplay { get; }

        public LinkMonsterDisplay(LinkMonsterModel linkMonster) : base(linkMonster)
        {
            Atk = linkMonster.Atk;
            LinkRating = linkMonster.LinkRating;
            LinkArrows = linkMonster.LinkArrows;
            LevelDisplay = $"Link {LinkRating}";
            AtkDefDisplay = $"ATK : {Atk}";
        }

    }
}
