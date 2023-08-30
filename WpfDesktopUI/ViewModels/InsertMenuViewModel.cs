using Caliburn.Micro;
using System;
using WpfDesktopUI.Library;
using WpfDesktopUI.Library.Models;

namespace WpfDesktopUI.ViewModels
{
    public class InsertMenuViewModel : Screen
    {

        public BindableCollection<string>? Sets { get; }

        public BindableCollection<CollectionCardDisplay>? CardsFromSet { get; set; }



        string? _selectedSet;
        public string? SelectedSet 
        { 
            get => _selectedSet; 
            set 
            { 
                _selectedSet = value;
                CardsFromSet = new BindableCollection<CollectionCardDisplay>(Mapper.GetCardsFromSet(SelectedSet));
                NotifyOfPropertyChange(nameof(CardsFromSet));
                
            }

        }

        public InsertMenuViewModel()
        {
            Sets = new BindableCollection<string>(Mapper.GetSetNames());
        }

    }
}