using ApiDataAccess.Library.Models;
using ApiDataAccess.Library.Models.Monsters;
using ApiDataAccess.Library.Models.NonMonsters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiDataAccess.Library.Helpers
{
    public class JsonCardConverter : JsonConverter
    {
        public static Dictionary<string, Type> StringTypePairs = new()
        {
            { "Pendulum", typeof(PendulumMonsterModel) },
            { "Link", typeof(LinkMonsterModel) },
            { "Monster", typeof(StandardMonsterModel) },
            { "Spell", typeof(SpellModel) },
            { "Trap", typeof(TrapModel) },
            { "Skill", typeof(SkillModel) },
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CardModel);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            string cardType = obj.Property("type")?.ToString();
            var typeToReturn = StringTypePairs.FirstOrDefault(x => cardType.Contains(x.Key)).Value;
            
            if (typeToReturn != null)
            {
                var target = Activator.CreateInstance(typeToReturn);

                serializer.Populate(obj.CreateReader(), target);

                return target;

            }

            else
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
