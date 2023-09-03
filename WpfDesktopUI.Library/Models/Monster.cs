using SqliteDataAccess.Library.DTOs;
using System;
using System.Linq;

namespace WpfDesktopUI.Library.Models
{
    public abstract class Monster : Card
    {

        private static readonly string[] FrameTypes =
        {
            "Normal",
            "Ritual",
            "Fusion",
            "Synchro",
            "XYZ",
            "Link",
        };

        public string Attribute { get; }

        public string Race { get; }

        public string[] Type { get; }

        public abstract string LevelDisplay { get; }

        public abstract string AtkDefDisplay { get; }

        public string RaceAndTypeDisplay { get; }

        public override string[] FrameType { get; }

        private static string[] GetFrameType(string[] type)
        {
            var result = FrameTypes.Intersect(type, StringComparer.OrdinalIgnoreCase).ToList();

            if (result.Count() == 0)
            {
                result.Add("Effect");
            }

            if (type.Any(t => t.Equals("Pendulum", StringComparison.OrdinalIgnoreCase)))
            {
                result.Add("Pendulum");
            }

            return result.ToArray();
        }

        public Monster(MonsterDTO monster) : base(monster)
        {
            Attribute = monster.Attribute;
            Race = monster.Race;
            Type = monster.Type;
            RaceAndTypeDisplay = $"[ {Race} / {string.Join(" / ", Type)} ]";
            FrameType = GetFrameType(Type);
        }

    }
}
