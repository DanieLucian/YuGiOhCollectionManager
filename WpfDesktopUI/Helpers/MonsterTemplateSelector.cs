using SqliteDataAccess.Library.Models;
using System.Windows;
using System.Windows.Controls;

namespace WpfDesktopUI.Helpers
{
    public class MonsterTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PendulumScaleTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case PendulumMonsterModel:
                    {
                        return PendulumScaleTemplate;
                    }

                default:
                    {
                        return null;
                    }
            }
        }

    }
}
