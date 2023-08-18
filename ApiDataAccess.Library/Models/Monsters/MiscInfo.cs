using Newtonsoft.Json;
using System;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class MiscInfo
    {

        // JsonProperty is used to map the class field to the JSON corresponding object field
        [JsonProperty("has_effect")]
        public bool HasEffect { get; set; }

        [JsonProperty("question_atk")]
        public bool HasQuestionAtk { get; set; }

        [JsonProperty("question_def")]
        public bool HasQuestionDef { get; set; }

        [JsonProperty("staple")]
        public string IsStaple { get; set; }

        [JsonProperty("tcg_date")]
        public DateTime TcgDate { get; set; }

    }
}

