using Caliburn.Micro;
using ExternalServices;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.ViewModels
{
    public class InsertMenuViewModel : Screen
    {

        private string? _selectedSet;

        public BindableCollection<string>? Sets { get; }

        public BindableCollection<CollectionCard> CardsFromSet { get; set; }
        public string? SelectedSet
        {
            get => _selectedSet;
            set
            {
                _selectedSet = value;
                CardsFromSet = new BindableCollection<CollectionCard>(Mapper.GetCardsFromSet(SelectedSet));
                NotifyOfPropertyChange(nameof(CardsFromSet));

            }

        }

        public int QuantityForAll { get; set; } = 0;

        public InsertMenuViewModel()
        {
            Sets = new BindableCollection<string>(Mapper.GetSetNames());
        }

        public async Task InsertCards()
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