using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models
{
    public abstract class Card
    {

        public long? Id { get; set; } = null;

        public string Name { get; set; } = null;

        [JsonProperty("desc")]
        public string Description { get; set; } = null;

    }
}