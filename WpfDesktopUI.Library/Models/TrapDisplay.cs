using SqliteDataAccess.Library.Models;

namespace WpfDesktopUI.Library.Models
{
    public class TrapDisplay : CardDisplay
    {

        public string Category { get; } = "TRAP";

        public string Icon { get; }

        public override string[] FrameType { get; } = { "Trap" };

        public TrapDisplay(TrapModel trap) : base(trap)
        {
            Icon = trap.Icon;
        }
    }
}
