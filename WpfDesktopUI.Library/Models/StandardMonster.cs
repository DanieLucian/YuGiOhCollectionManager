using SqliteDataAccess.Library.DTOs;
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

        private static string GetLevelDisplay(string[] type, byte value)
        {
            if (type.Any(t => t.Equals("XYZ", StringComparison.OrdinalIgnoreCase)))
            {
                return $"Rank {value}";
            }

            return $"Level {value}";
        }

        public StandardMonster(StandardMonsterDTO standardMonster) 
            : base(standardMonster)
        {
            Atk = standardMonster.Atk;
            Def = standardMonster.Def;
            LvlRank = standardMonster.LvlRank;
            LevelDisplay = GetLevelDisplay(Type, LvlRank);
            AtkDefDisplay = $"ATK / DEF : {Atk} / {Def}";
        }
    }
}
