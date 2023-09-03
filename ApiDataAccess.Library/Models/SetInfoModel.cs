using Newtonsoft.Json;

namespace ApiDataAccess.Library.Models;

public class SetInfoModel
{

    [JsonProperty("set_name")]
    public string SetName { get; set; }

    [JsonProperty("set_code")]
    public string SetCode { get; set; }

    [JsonProperty("set_rarity")]
    public string RarityName { get; set; }

    [JsonProperty("set_rarity_code")]
    public string RarityCode { get; set; }
}