using SqliteDataAccess.Library.DTOs;

namespace WpfDesktopUI.Library.Models
{
    public class Spell : Card
    {

        public string Category { get; } = "SPELL";

        public string Icon { get; }

        public override string[] FrameType { get; } = { "Spell" };

        public Spell(SpellModel spell) : base(spell)
        {
            Icon = spell.Icon;
        }
    }
}
