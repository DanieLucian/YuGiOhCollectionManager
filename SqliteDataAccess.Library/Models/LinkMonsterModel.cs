namespace SqliteDataAccess.Library.Models
{
    public class LinkMonsterModel : MonsterModel
    {
        public string Atk { get; set; }

        public byte LinkRating { get; set; }

        public string[] LinkArrows { get; set; }
    }
}
