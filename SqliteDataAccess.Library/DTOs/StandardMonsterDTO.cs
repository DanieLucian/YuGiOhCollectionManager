using System.Linq;

namespace SqliteDataAccess.Library.DTOs
{
    public class StandardMonsterDTO : MonsterDTO
    {

        public string Atk { get; set; }

        public string Def { get; set; }

        public byte LvlRank { get; set; }
    }
}
