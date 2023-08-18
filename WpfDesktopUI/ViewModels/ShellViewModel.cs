using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfDesktopUI.Library.Models;
using WpfDesktopUI.Library;

namespace WpfDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object> // everything can go into the conductor 
    {

        private readonly StringComparison noCase = StringComparison.OrdinalIgnoreCase;

        private readonly List<CardDisplay> cards;

        private readonly sbyte pageSize = 15;

        private int numberOfPages;

        private string _filterName = string.Empty;

        private string _filterType = string.Empty;

        private int _currentPageIndex = 0;

        private List<CardDisplay[]> _filteredCards;

        List<CardDisplay[]> FilteredCards
        {
            get => _filteredCards;
            set
            {
                _filteredCards = value;
                NotifyOfPropertyChange(nameof(FilteredCards));
                numberOfPages = FilteredCards.Count;
            }
        }

        public int CurrentPageIndex
        {
            get => _currentPageIndex;
            set
            {
                _currentPageIndex = value;
                NotifyOfPropertyChange(nameof(CanGoToNextPage));
                NotifyOfPropertyChange(nameof(CanGoToPreviousPage));
                CurrentPage = numberOfPages > 0 ? FilteredCards[CurrentPageIndex] : Array.Empty<CardDisplay>();
                NotifyOfPropertyChange(nameof(CurrentPage));
            }
        }

        public IEnumerable<CardDisplay> CurrentPage { get; set; }

        public string FilterName
        {
            get => _filterName;
            set
            {
                _filterName = value;
                NotifyOfPropertyChange(nameof(CurrentPage));
            }
        }

        public string FilterType
        {
            get => _filterType;
            set
            {
                _filterType = value;
                NotifyOfPropertyChange(nameof(FilterType));
            }
        }

        public bool CanGoToNextPage => CurrentPageIndex < numberOfPages - 1;

        public bool CanGoToPreviousPage => CurrentPageIndex > 0;

        /// <summary>
        ///  Initializes the Main Window:  1. Loads the entire collection of cards in a List. 2. A CollectionView copies
        ///  that bindable collection. This is what the UI is actually binded to. 3. The CollectionView supports
        ///  sorting, filtering and grouping of data with real time response from UI.
        /// </summary>
        public ShellViewModel()
        {
            Mapper.UpdateDatabase().AsResult();
            cards = Mapper.Map().Result;
            FilterCards();
        }

        public void FilterCards()
        {
            FilteredCards = cards
                .Where(x => x.Name.Contains(FilterName, noCase) && x.Description.Contains(FilterType, noCase))
                .Chunk(pageSize)
                .ToList();
            CurrentPageIndex = 0;
        }

        public void GoToNextPage()
        {
            CurrentPageIndex++;
        }

        public void GoToPreviousPage()
        {
            CurrentPageIndex--;
        }

    }
}

