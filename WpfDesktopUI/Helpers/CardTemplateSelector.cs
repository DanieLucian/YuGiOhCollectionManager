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
                case Library.Models.StandardMonster:
                    {
                        if (item is PendulumMonster)
                        { 
                            return PendulumMonster; 
                        }

                        return StandardMonster;
                    }

                case Library.Models.LinkMonster:
                    {
                        return LinkMonster;
                    }

                case Spell:
                case Trap:
                    {
                        return SpellTrap;
                    }

                case Library.Models.Skill:
                    {
                        return Skill;
                    }
            }

            return null;
        }

    }
}
