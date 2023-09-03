using SqliteDataAccess.Library.DTOs;

namespace WpfDesktopUI.Library.Models
{
    public class Skill : Card
    {

        public string Category { get; } = "SKILL";

        public string Character { get; }

        public override string[] FrameType { get; } = { "Skill" };

        public Skill(SkillDTO skill) : base(skill)
        {
            Character = skill.Character;
        }
    }
}
