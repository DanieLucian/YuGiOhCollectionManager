using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class LinkMonster : Monster, IWithLink
    {
        public override string Def { get; set; } = "-";

        [JsonProperty("linkval")]
        public byte LinkRating { get; set; }

        [JsonProperty("linkmarkers")]
        public IEnumerable<string> LinkArrows { get; set; }

    }
}