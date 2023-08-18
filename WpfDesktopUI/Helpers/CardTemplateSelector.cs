using SqliteDataAccess.Library.Models;
using System.Windows;
using System.Windows.Controls;


namespace WpfDesktopUI.Helpers
{
    internal class CardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StandardMonster { get; set; }

        public DataTemplate PendulumMonster { get; set; }

        public DataTemplate LinkMonster { get; set; }

        public DataTemplate SpellTrap { get; set; }

        public DataTemplate Skill { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch(item)
            {
                case StandardMonsterModel:
                    {
                        if (item is PendulumMonsterModel)
                        { 
                            return PendulumMonster; 
                        }

                        return StandardMonster;
                    }

                case LinkMonsterModel:
                    {
                        return LinkMonster;
                    }

                case SpellModel:
                case TrapModel:
                    {
                        return SpellTrap;
                    }

                case SkillModel:
                    {
                        return Skill;
                    }
            }

            return null;
        }

    }
}
