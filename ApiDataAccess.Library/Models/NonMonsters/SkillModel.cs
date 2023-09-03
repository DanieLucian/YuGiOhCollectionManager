using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.NonMonsters
{
    public class SkillModel : CardModel
    {

        [JsonProperty("race")]
        public string Character { get; set; }

    }
}