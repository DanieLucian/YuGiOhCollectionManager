using Caliburn.Micro;
using ExternalServices;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.ViewModels
{
    public class InsertMenuViewModel : Screen
    {

        public BindableCollection<string>? Sets { get; }

        public BindableCollection<CollectionCard> CardsFromSet { get; set; }

        string? _selectedSet;
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

        public InsertMenuViewModel()
        {
            Sets = new BindableCollection<string>(Mapper.GetSetNames());
        }

        public async Task InsertCards()
        {
            var aboveZero = CardsFromSet.Where(x => x.Quantity > 0);
                                       
            var rowsAffected = await Mapper.UpdateCardQuantity(aboveZero);

        }

    }
}