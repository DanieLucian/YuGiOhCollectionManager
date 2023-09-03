using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.NonMonsters
{
    public class SpellModel : CardModel
    {

        [JsonProperty("race")]
        public string SpellIcon { get; set; }

    }
}