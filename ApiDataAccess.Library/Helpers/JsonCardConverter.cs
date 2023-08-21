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
            { "Pendulum", typeof(PendulumMonster) },
            { "Link", typeof(LinkMonster) },
            { "Monster", typeof(StandardMonster) },
            { "Spell", typeof(Spell) },
            { "Trap", typeof(Trap) },
            { "Skill", typeof(Skill) }
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Card);
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

                //return obj.ToObject(typeToReturn, serializer);
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
