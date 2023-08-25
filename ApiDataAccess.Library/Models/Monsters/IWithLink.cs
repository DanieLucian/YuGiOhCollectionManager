using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiDataAccess.Library.Models.Monsters
{
    public interface IWithLink
    {
        [JsonProperty("linkval")]
        byte LinkRating { get; }

        [JsonProperty("linkmarkers")]
        IEnumerable<string> LinkArrows { get; }
    }
}