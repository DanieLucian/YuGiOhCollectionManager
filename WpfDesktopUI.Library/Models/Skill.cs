namespace WpfDesktopUI.Library.Models
{
    public class Skill : Card
    {

        public string Category { get; } = "SKILL";

        public string Character { get; }

        public override string[] FrameType { get; } = { "Skill" };

        public Skill(string name, string desc, string character) : base(name, desc)
        {
            Character = character;
        }
    }
}
