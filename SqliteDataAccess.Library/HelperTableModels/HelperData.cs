
using System.Collections.Generic;

namespace SqliteDataAccess.Library.HelperTableModels
{
    public class HelperData
    {

        public IEnumerable<AttributeModel> Attributes { get; set; }

        public IEnumerable<RaceModel> Races { get; set; }

        public IEnumerable<TypeModel> Types { get; set; }

        public IEnumerable<LinkArrowModel> LinkArrows { get; set; }

        public IEnumerable<SpellIconModel> SpellIcons { get; set; }

        public IEnumerable<TrapIconModel> TrapIcons { get; set; }

        public IEnumerable<FullSetModel> Sets { get; set; }

    }
}
