using SqliteDataAccess.Library.Models;

namespace WpfDesktopUI.Library.Models
{
    public class SpellDisplay : CardDisplay
    {

        public string Category { get; } = "SPELL";

        public string Icon { get; }

        public override string[] FrameType { get; } = { "Spell" };

        public SpellDisplay(SpellModel spell) : base(spell)
        {
            Icon = spell.Icon;
        }
    }
}
