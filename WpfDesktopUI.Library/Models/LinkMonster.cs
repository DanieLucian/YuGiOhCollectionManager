using SqliteDataAccess.Library.DTOs;

namespace WpfDesktopUI.Library.Models
{
    public class LinkMonster : Monster
    {
        public string Atk { get; }

        public byte LinkRating { get; }

        public string[] LinkArrows { get; }

        public override string LevelDisplay { get; }

        public override string AtkDefDisplay { get; }

        public LinkMonster(LinkMonsterDTO linkMonster) : base(linkMonster)
        {
            Atk = linkMonster.Atk;
            LinkRating = linkMonster.LinkRating;
            LinkArrows = linkMonster.LinkArrows;
            LevelDisplay = $"Link {LinkRating}";
            AtkDefDisplay = $"ATK : {Atk}";
        }

    }
}
