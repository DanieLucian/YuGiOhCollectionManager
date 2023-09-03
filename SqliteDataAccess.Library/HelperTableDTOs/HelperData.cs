
using System.Collections.Generic;

namespace SqliteDataAccess.Library.HelperTableDTOs
{
    public class HelperData
    {

        public IEnumerable<AttributeDTO> Attributes { get; set; }

        public IEnumerable<RaceModelDTO> Races { get; set; }

        public IEnumerable<TypeDTO> Types { get; set; }

        public IEnumerable<LinkArrowModel> LinkArrows { get; set; }

        public IEnumerable<SpellIconDTO> SpellIcons { get; set; }

        public IEnumerable<TrapIconDTO> TrapIcons { get; set; }

        public IEnumerable<FullSetDTO> Sets { get; set; }

    }
}
