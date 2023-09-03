namespace SqliteDataAccess.Library.DTOs
{
    public class LinkMonsterDTO : MonsterDTO
    {
        public string Atk { get; set; }

        public byte LinkRating { get; set; }

        public string[] LinkArrows { get; set; }
    }
}
