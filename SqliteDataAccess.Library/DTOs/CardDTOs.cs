using System;
using System.Collections.Generic;
using System.Text;

namespace SqliteDataAccess.Library.DTOs
{
    public class CardDTOs
    {

        public IEnumerable<StandardMonsterDTO> StandardMonsterDTOs { get; set; }

        public IEnumerable<PendulumMonsterDTO> PendulumMonsterDTOs { get; set; }

        public IEnumerable<LinkMonsterDTO> LinkMonsterDTOs { get; set; }

        public IEnumerable<SpellDTO> SpellDTOs { get; set; }

        public IEnumerable<TrapDTO> TrapDTOs { get; set; }

        public IEnumerable<SkillDTO> SkillDTOs { get; set; }

    }
}
