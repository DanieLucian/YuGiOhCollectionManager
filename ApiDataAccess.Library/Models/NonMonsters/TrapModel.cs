using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.NonMonsters
{
    public class TrapModel : CardModel
    {

        [JsonProperty("race")]
        public string TrapIcon { get; set; }

    }
}