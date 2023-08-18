using SqliteDataAccess.Library.Models;

namespace WpfDesktopUI.Library.Models
{
    public class SkillDisplay : CardDisplay
    {

        public string Category { get; } = "SKILL";

        public string Character { get; }

        public override string[] FrameType { get; } = { "Skill" };

        public SkillDisplay(SkillModel skill) : base(skill)
        {
            Character = skill.Character;
        }
    }
}
