using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteDataAccess.Library.DTOs
{
    public class CardDTOs
    {

        public IEnumerable<StandardMonsterDTO> StandardMonsters { get; set; }

        public IEnumerable<PendulumMonsterDTO> PendulumMonsters { get; set; }

        public IEnumerable<LinkMonsterDTO> LinkMonsters { get; set; }

        public IEnumerable<SpellModel> Spells { get; set; }

        public IEnumerable<TrapDTO> Traps { get; set; }

        public IEnumerable<SkillDTO> Skills { get; set; }

    }
}
