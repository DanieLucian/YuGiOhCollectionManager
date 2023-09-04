namespace WpfDesktopUI.Library.Models
{
    public class Trap : Card
    {

        public string Category { get; } = "TRAP";

        public string Icon { get; }

        public override string[] FrameType { get; } = { "Trap" };

        public Trap(string name, string desc, string icon) : base(name, desc)
        {
            Icon = icon;
        }

    }
}
