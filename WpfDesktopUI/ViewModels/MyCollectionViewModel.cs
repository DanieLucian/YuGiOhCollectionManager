using Caliburn.Micro;
using ExternalServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfDesktopUI.Library;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.ViewModels
{
    public class MyCollectionViewModel : UserControl
    {
        public BindableCollection<CollectionCard>? MyCollection { get; set; }

        private readonly WindowManager windowManager = new();

        public async Task LoadCollectionAsync()
        {
            
            MyCollection = new BindableCollection<CollectionCard>(await Mapper.GetCollection());
        }

        public async Task OpenInsertMenu()
        {
            await windowManager.ShowDialogAsync(new InsertMenuViewModel());
        }
    }
}