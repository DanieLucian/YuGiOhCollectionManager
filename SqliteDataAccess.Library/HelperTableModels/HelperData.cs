using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDataAccess.Library.HelperTableModels
{
    public class HelperData
    {

        public List<AttributeModel> Attributes { get; set; } = null;

        public List<RaceModel> Races { get; set; } = null;

        public List<TypeModel> Types { get; set; } = null;

        public List<LinkArrowModel> LinkArrows { get; set; } = null;

        public List<SpellIconModel> SpellIcons { get; set; } = null;

        public List<TrapIconModel> TrapIcons { get; set; } = null;

    }
}
