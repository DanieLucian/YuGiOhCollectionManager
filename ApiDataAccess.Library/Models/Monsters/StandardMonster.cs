using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class StandardMonster : Monster, IWithLevelOrRank
    {
        [JsonProperty("level")]
        public byte LvlRank { get; set; }
    }
}