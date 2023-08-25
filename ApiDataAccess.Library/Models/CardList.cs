using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiDataAccess.Library.Models
{
    public class CardList
    {

        [JsonProperty("data")]
        public IEnumerable<Card> AllCards { get; set; }

    }
}

