using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiDataAccess.Library.Models;

public abstract class Card
{

    public int Id { get; set; }

    public string Name { get; set; }

    [JsonProperty("desc")]
    public string Description { get; set; }

    [JsonProperty("card_sets")]
    public IEnumerable<SetInfo> SetInfo { get; set; }

    [JsonProperty("misc_info")]
    public List<ExtraInfo> ExtraInfo { get; set; }

}