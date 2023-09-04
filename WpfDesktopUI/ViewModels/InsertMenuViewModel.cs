using Caliburn.Micro;
using ExternalServices;
using System;
using System.Linq;
using WpfDesktopUI.Library;
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

        public void InsertCards()
        {
            var aboveZero = CardsFromSet.Where(x => x.Quantity > 0)
                                        .Select(y => new
                                                     {
                                                         y.SetId,
                                                         y.CardId,
                                                         y.RarityName,
                                                         y.Quantity
                                                     });




        }

    }
}