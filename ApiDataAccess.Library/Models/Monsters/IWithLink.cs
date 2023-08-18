using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public interface IWithLink
    {
        [JsonProperty("linkval")]
        byte LinkRating { get; }

        [JsonProperty("linkmarkers")]
        string[] LinkArrows { get; }
    }
}