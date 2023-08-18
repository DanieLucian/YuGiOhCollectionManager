using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.NonMonsters
{
    public class Trap : Card
    {

        [JsonProperty("race")]
        public string TrapIcon { get; set; }

    }
}