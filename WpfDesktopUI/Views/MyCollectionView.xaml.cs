using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDesktopUI.Views
{
    /// <summary>
    /// Interaction logic for MyCollectionView.xaml
    /// </summary>
    public partial class MyCollectionView : UserControl
    {
        public MyCollectionView()
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
