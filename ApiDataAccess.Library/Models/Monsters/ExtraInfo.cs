using Newtonsoft.Json;
using System;

namespace ApiDataAccess.Library.Models.Monsters
{
    public class ExtraInfo
    {

        // JsonProperty is used to map the class field to the JSON corresponding object field
        [JsonProperty("formats")]
        public string[] Formats { get; set; }

        [JsonProperty("has_effect")]
        public bool HasEffect { get; set; }

        [JsonProperty("question_atk")]
        public bool HasQuestionAtk { get; set; }

        [JsonProperty("question_def")]
        public bool HasQuestionDef { get; set; }

    }
}

