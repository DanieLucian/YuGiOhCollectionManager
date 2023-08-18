

namespace SqliteDataAccess.Library.Models
{
    public abstract class MonsterModel : CardModel
    {

        public string Attribute { get; set; }

        public string Race { get; set; }

        public string[] Type { get; set; }

    }
}
