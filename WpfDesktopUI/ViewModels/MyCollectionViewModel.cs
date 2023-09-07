using Caliburn.Micro;
using ExternalServices;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.ViewModels
{
    public class MyCollectionViewModel : Screen
    {
        private BindableCollection<CollectionCard>? _myCollection;

        public BindableCollection<CollectionCard>? MyCollection
        {
            get => _myCollection;
            set
            {
                _myCollection = value;
                NotifyOfPropertyChange(nameof(MyCollection));
            }
        }

        private readonly WindowManager windowManager = new();

        public async Task LoadCollectionAsync()
        {
            MyCollection = new BindableCollection<CollectionCard>(await Mapper.GetCollection());
        }

        public async Task OpenInsertMenu()
        {
            await windowManager.ShowDialogAsync(new InsertMenuViewModel());
        }

        public void SerializeCollection()
        {
            Mapper.SerializeCollection(MyCollection);
        }
    }
}