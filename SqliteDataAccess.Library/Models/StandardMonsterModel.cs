using System.Linq;

namespace SqliteDataAccess.Library.Models
{
    public class StandardMonsterModel : MonsterModel
    {

        public string Atk { get; set; }

        public string Def { get; set; }

        public byte LvlRank { get; set; }
    }
}
