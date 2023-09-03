

namespace SqliteDataAccess.Library.DTOs
{
    public abstract class MonsterDTO : CardDTO
    {

        public string Attribute { get; set; }

        public string Race { get; set; }

        public string[] Type { get; set; }

    }
}
