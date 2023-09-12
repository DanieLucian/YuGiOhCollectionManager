using Caliburn.Micro;
using ExternalServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.ViewModels
{
    public class InsertMenuViewModel : Screen
    {

        private string? _selectedSet;

        string _cardNameFilter;

        public short AmountOfCards => (short)(CardsFromSet is null ? 0 : CardsFromSet.Count);

        public BindableCollection<string>? Sets { get; }

        public BindableCollection<CollectionCard> CardsFromSet { get; set; }

        public string CardNameFilter
        {
            get => _cardNameFilter;
            set
            {
                _cardNameFilter = value;
                if (CardNameFilter != string.Empty)
                {
                    SelectedCard = CardsFromSet.FirstOrDefault(x => x.CardName
                                                                     .Contains(CardNameFilter, StringComparison.OrdinalIgnoreCase));
                    NotifyOfPropertyChange(nameof(CardNameFilter));
                    NotifyOfPropertyChange(nameof(SelectedCard));
                }
            }
        }

        public CollectionCard? SelectedCard { get; set; }

        public string? SelectedSet
        {
            get => _selectedSet;
            set
            {
                _selectedSet = value;
                CardsFromSet = new BindableCollection<CollectionCard>(Mapper.GetCardsFromSet(SelectedSet));
                SelectedCard = null;
                NotifyOfPropertyChange(nameof(CardsFromSet));
                NotifyOfPropertyChange(nameof(AmountOfCards));
                NotifyOfPropertyChange(nameof(SelectedCard));
            }
        }

        public int QuantityForAll { get; set; }

        public InsertMenuViewModel()
        {
            Sets = new BindableCollection<string>(Mapper.GetSetNames());
        }

        public InsertMenuViewModel(string? selectedSet)
        {
            SelectedSet = selectedSet;
            QuantityForAll = 0;
            SelectedCard = null;
        }

        public async Task UpdateCollection()
        {
            var nonZeroQty = CardsFromSet.Where(x => x.Quantity != 0);

            var rowsAffected = await Mapper.UpdateCardQuantity(nonZeroQty);

            foreach (var card in CardsFromSet.Where(x => x.Quantity != 0))
            {
                card.CurrentQuantity += card.Quantity;
                card.Quantity = 0;
            }

            CardsFromSet.Refresh();
        }

        public void SetQuantityToMultiple()
        {
            foreach (var card in CardsFromSet)
            {
                card.Quantity = card.CurrentQuantity + QuantityForAll < 0 ? -card.CurrentQuantity : QuantityForAll;
            }

            CardsFromSet.Refresh();
        }

    }
}