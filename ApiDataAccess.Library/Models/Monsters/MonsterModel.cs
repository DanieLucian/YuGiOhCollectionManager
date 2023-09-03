using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ApiDataAccess.Library.Models.Monsters
{
    public abstract class MonsterModel: CardModel, IWithAtk, IWithDef
    {
        private static readonly string[] values = { "Effect", "Normal", "Token" };

        public string Attribute { get; set; }

        public string Race { get; set; }

        [JsonProperty("type")]
        public string JsonType { get; set; }

        public IEnumerable<string> Type => GetCardType(JsonType, ExtraInfo);

        public string Atk { get; set; }

        public virtual string Def { get; set; }

        public static IEnumerable<string> GetCardType(string jsonCardType, List<ExtraInfoModel> info)
        {
            List<string> result = jsonCardType.Split(' ').ToList();
            
            _ = result.Remove("Monster");

            if (info[0].HasEffect)
            {
                if(result.Intersect(values) is null)
                {
                    result.Add("Effect");
                }
            }

            return result.ToArray();
        }

    }
}