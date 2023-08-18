using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class PendulumMonster : StandardMonster, IWithScale
    {
        [JsonProperty("scale")]
        public byte Scale { get; set; }
    }
}