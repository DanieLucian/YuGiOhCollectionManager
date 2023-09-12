using Caliburn.Micro;
using ExternalServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.ViewModels
{
    public class MyCollectionViewModel : Screen
    {
        private string _cardNameFilter;

        public string CardNameFilter
        {
            get => _cardNameFilter;
            set
            {
                _cardNameFilter = value;
                if (CardNameFilter != string.Empty)
                {
                    SelectedCard = MyCollection.FirstOrDefault(x => x.CardName
                                                                     .Contains(CardNameFilter, StringComparison.OrdinalIgnoreCase));
                    NotifyOfPropertyChange(nameof(CardNameFilter));
                    NotifyOfPropertyChange(nameof(SelectedCard));
                }
            }
        }

        public CollectionCard? SelectedCard { get; set; }


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