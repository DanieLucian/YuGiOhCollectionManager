using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfDesktopUI.Views
{
    /// <summary>
    ///  Interaction logic for InsertMenuView.xaml
    /// </summary>
    public partial class InsertMenuView : UserControl
    {
        public InsertMenuView()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                grid.ScrollIntoView(grid.SelectedItem);
            }
        }

    }
}
