using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteDataAccess.Library.Models
{
    public class CardModels
    {

        public IEnumerable<StandardMonsterModel> StandardMonsters { get; set; }

        public IEnumerable<PendulumMonsterModel> PendulumMonsters { get; set; }

        public IEnumerable<LinkMonsterModel> LinkMonsters { get; set; }

        public IEnumerable<SpellModel> Spells { get; set; }

        public IEnumerable<TrapModel> Traps { get; set; }

        public IEnumerable<SkillModel> Skills { get; set; }

    }
}
