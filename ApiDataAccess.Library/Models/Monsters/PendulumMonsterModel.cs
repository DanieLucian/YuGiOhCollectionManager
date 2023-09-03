using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class PendulumMonsterModel : StandardMonsterModel, IWithScale
    {
        [JsonProperty("scale")]
        public byte Scale { get; set; }
    }
}