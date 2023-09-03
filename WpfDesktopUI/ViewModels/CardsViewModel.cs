﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfDesktopUI.Library.Models;
using WpfDesktopUI.Library;

namespace WpfDesktopUI.ViewModels
{
    public class CardsViewModel : Screen
    {
        private readonly StringComparison noCase = StringComparison.OrdinalIgnoreCase;

        private IEnumerable<Card> cards = new List<Card>();

        private readonly sbyte pageSize = 10;

        private int numberOfPages;

        private string _filterName = string.Empty;

        private string _filterType = string.Empty;

        private int _currentPageIndex = 0;

        private List<Card[]> _filteredCards = new();

        public List<Card[]> FilteredCards
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
                CurrentPage = numberOfPages > 0 ? FilteredCards[CurrentPageIndex] : Array.Empty<Card>();
                NotifyOfPropertyChange(nameof(CurrentPage));
            }
        }

        public IEnumerable<Card>? CurrentPage { get; set; }

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
        public CardsViewModel()
        {

        }

        public async Task LoadDataAsync()
        {
            cards = await Mapper.ToCardDisplay();
            FilteredCards = await Task.Run(() => cards.Chunk(pageSize).ToList());
            CurrentPageIndex = 0;
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
