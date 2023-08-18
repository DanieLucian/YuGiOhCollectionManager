using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ApiDataAccess.Library.Models.Monsters
{
    public abstract class Monster: Card, IWithAtk, IWithDef
    {
        private static readonly string[] values = { "Effect", "Normal", "Token" };

        public string Attribute { get; set; }

        public string Race { get; set; }

        [JsonProperty("type")]
        public string JsonType { get; set; }

        public string[] Type => GetCardType(JsonType, MiscInfo);

        [JsonProperty("misc_info")]
        public MiscInfo[] MiscInfo { get; set; }

        public string Atk { get; set; }

        public virtual string Def { get; set; }

        public static string[] GetCardType(string jsonCardType, MiscInfo[] miscInfo)
        {
            List<string> result = jsonCardType.Split(' ').ToList();
            
            _ = result.Remove("Monster");

            if (miscInfo[0].HasEffect)
            {
                /*if (!result.Contains("Effect", StringComparer.OrdinalIgnoreCase) &&
                    !result.Contains("Normal", StringComparer.OrdinalIgnoreCase) &&
                    !result.Contains("Token", StringComparer.OrdinalIgnoreCase)
                   )*/
                if(result.Intersect(values) is null)
                {
                    result.Add("Effect");
                }
            }

            return result.ToArray();
        }

    }
}