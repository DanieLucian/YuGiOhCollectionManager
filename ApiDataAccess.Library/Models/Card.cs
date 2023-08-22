using ApiDataAccess.Library.Models.Monsters;
using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models
{
    public abstract class Card
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("misc_info")]
        public ExtraInfo[] ExtraInfo { get; set; }

    }
}