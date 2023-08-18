using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public interface IWithLevelOrRank
    {
        [JsonProperty("level")]
        byte LvlRank { get; set; }
    }
}