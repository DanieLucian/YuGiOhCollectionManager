using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class LinkMonster : Monster, IWithLink
    {
        public override string Def { get; set; } = "-";

        [JsonProperty("linkval")]
        public byte LinkRating { get; set; }

        [JsonProperty("linkmarkers")]
        public string[] LinkArrows { get; set; }

    }
}