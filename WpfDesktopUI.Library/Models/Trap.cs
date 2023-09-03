using SqliteDataAccess.Library.DTOs;

namespace WpfDesktopUI.Library.Models
{
    public class Trap : Card
    {

        public string Category { get; } = "TRAP";

        public string Icon { get; }

        public override string[] FrameType { get; } = { "Trap" };

        public Trap(TrapDTO trap) : base(trap)
        {
            Icon = trap.Icon;
        }
    }
}
