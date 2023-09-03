using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiDataAccess.Library.Models
{
    public class CardList
    {

        [JsonProperty("data")]
        public IEnumerable<CardModel> AllCards { get; set; }

    }
}

