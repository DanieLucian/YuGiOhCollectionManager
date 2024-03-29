﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfDesktopUI.Library;
using WpfDesktopUI.Library.Models;
using Logger.Library;
using System.Threading.Tasks;
using ApiDataAccess.Library.Helpers;
using System.Threading;

namespace WpfDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive // everything can go into the conductor 
    {
        private readonly StringComparison noCase = StringComparison.OrdinalIgnoreCase;

        private IList<CardDisplay> cards;

        private readonly sbyte pageSize = 10;

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
                Log.Info("The current page has been updated! Ready to be displayed!");
                NotifyOfPropertyChange(nameof(CurrentPage));
            }
        }

        public IEnumerable<CardDisplay>? CurrentPage { get; set; }

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
            ApiHelper.InitializeClient();
        }

        public async Task OnViewLoaded()
        {
            await Log.InitInfo();
            await Mapper.UpdateDatabase();
            await Log.Info("Database has been updated");
            cards = await Mapper.Map();
            await Log.Info("AllCards from DB has been mapped to DisplayObjects");
            //Log.Info("AllCards from DB has been mapped to DisplayObjects");

            // FilteredCards = cards.Chunk(pageSize).ToList();
            FilterCards();
            await Log.Info("AllCards is ready to be displayed!");
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

        public void LoadCollection()
        {
            ActivateItemAsync(new CollectionViewModel());
            //this.CloseItemAsync(this);
        }
    }
}

