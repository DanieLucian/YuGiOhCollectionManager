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
        private bool _showAll = false;

        private string _cardNameFilter = string.Empty;

        private string _setNameFilter = string.Empty;

        public string SetNameFilter
        {
            get => _setNameFilter;
            set
            {
                _setNameFilter = value;
                FilterBySet();
                NotifyOfPropertyChange(nameof(SetNameFilter));
                NotifyOfPropertyChange(nameof(SelectedCard));
            }
        }

        private BindableCollection<CollectionCard> _myCollection;

        private BindableCollection<CollectionCard> allCardsCollection = new();

        private readonly WindowManager windowManager = new();

        public bool ShowAll
        {
            get => _showAll;
            set
            {
                _showAll = value;
                FilterBySet();
            }
        }

        public int TotalOwnedCards => MyCollection is null ? 0 : MyCollection.Sum(x => x.CurrentQuantity);

        public int TotalUniqueOwnedCards => MyCollection is null ? 0 : MyCollection.Count(x => x.CurrentQuantity > 0);

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

        public BindableCollection<CollectionCard> MyCollection
        {
            get => _myCollection;
            set
            {
                _myCollection = value;
                NotifyOfPropertyChange(nameof(MyCollection));
                NotifyOfPropertyChange(nameof(TotalOwnedCards));
                NotifyOfPropertyChange(nameof(TotalUniqueOwnedCards));
            }
        }

        public async Task LoadCollectionAsync()
        {
            allCardsCollection = new BindableCollection<CollectionCard>(await Mapper.GetCollection());
        }

        public async Task OpenInsertMenu()
        {
            await windowManager.ShowDialogAsync(new InsertMenuViewModel());
        }

        public void SerializeCollection()
        {
            Mapper.SerializeCollection(MyCollection);
        }

        public async Task UpdateCollection()
        {
            var nonZeroQty = MyCollection.Where(x => x.Quantity != 0);

            if (nonZeroQty.Any())
            {
                var rowsAffected = await Mapper.UpdateCardQuantity(nonZeroQty);
            }

            foreach (var card in MyCollection.Where(x => x.Quantity != 0))
            {
                card.CurrentQuantity += card.Quantity;
                card.Quantity = 0;
            }

            MyCollection.Refresh();
            NotifyOfPropertyChange(nameof(TotalOwnedCards));
            NotifyOfPropertyChange(nameof(TotalUniqueOwnedCards));
        }

        private void FilterBySet()
        {
            MyCollection = new BindableCollection<CollectionCard>(allCardsCollection.Where(x => x.SetName
                                                                                                 .Contains(SetNameFilter, StringComparison.OrdinalIgnoreCase) &&
                                                                                                (ShowAll == true ||
                                                                                                 x.CurrentQuantity > 0)));

        }

    }
}