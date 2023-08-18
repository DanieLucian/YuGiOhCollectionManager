using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.NonMonsters
{
    public class Skill : Card
    {

        [JsonProperty("race")]
        public string Character { get; set; }

    }
}