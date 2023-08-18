using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.NonMonsters
{
    public class Spell : Card
    {

        [JsonProperty("race")]
        public string SpellIcon { get; set; }

    }
}