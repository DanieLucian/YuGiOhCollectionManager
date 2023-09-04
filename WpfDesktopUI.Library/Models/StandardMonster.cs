using System;
using System.Linq;

namespace WpfDesktopUI.Library.Models
{
    public class StandardMonster : Monster
    {

        public string Atk { get; set; }

        public string Def { get; set; }

        public byte LvlRank { get; set; }

        public override string LevelDisplay { get; }

        public override string AtkDefDisplay { get; }

        public StandardMonster(
               string name,
               string desc,
               string attribute,
               string race,
               string[] type,
               string atk,
               string def,
               byte lvlRank) : base(name, desc, attribute, race, type)
        {
            Atk = atk;
            Def = def;
            LvlRank = lvlRank;
            LevelDisplay = GetLevelDisplay(Type, LvlRank);
            AtkDefDisplay = $"ATK / DEF : {Atk} / {Def}";

        }

        private static string GetLevelDisplay(string[] type, byte value)
        {
            if (type.Any(t => t.Equals("XYZ", StringComparison.OrdinalIgnoreCase)))
            {
                return $"Rank {value}";
            }

            return $"Level {value}";
        }
    }
}
