using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class StandardMonsterModel : MonsterModel, IWithLevelOrRank
    {
        [JsonProperty("level")]
        public byte LvlRank { get; set; }
    }
}