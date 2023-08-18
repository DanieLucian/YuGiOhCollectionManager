using System.Windows;
using System.Windows.Controls;
using WpfDesktopUI.Library.Models;


namespace WpfDesktopUI.Helpers
{
    internal class CardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? StandardMonster { get; set; }

        public DataTemplate? PendulumMonster { get; set; }

        public DataTemplate? LinkMonster { get; set; }

        public DataTemplate? SpellTrap { get; set; }

        public DataTemplate? Skill { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            switch(item)
            {
                case StandardMonsterDisplay:
                    {
                        if (item is PendulumMonsterDisplay)
                        { 
                            return PendulumMonster; 
                        }

                        return StandardMonster;
                    }

                case LinkMonsterDisplay:
                    {
                        return LinkMonster;
                    }

                case SpellDisplay:
                case TrapDisplay:
                    {
                        return SpellTrap;
                    }

                case SkillDisplay:
                    {
                        return Skill;
                    }
            }

            return null;
        }

    }
}
