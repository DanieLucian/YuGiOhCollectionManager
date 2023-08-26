using ApiDataAccess.Library.Helpers;
using Caliburn.Micro;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WpfDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive // everything can go into the conductor 
    {

        private readonly CardsViewModel _cardsViewModel;
        private readonly CollectionViewModel _collectionViewModel;

        public ShellViewModel(CardsViewModel cardsViewModel, CollectionViewModel collectionViewModel)
        {
            ApiHelper.InitializeClient();

            _cardsViewModel = cardsViewModel;
            _collectionViewModel = collectionViewModel;
        }

        public async Task OnViewLoaded()
        {
            await _cardsViewModel.LoadData();
        }

        public void LoadCollection()
        {
            ActivateItemAsync(_collectionViewModel);
            //this.CloseItemAsync(this);
        }

        public void LoadCards()
        {
            ActivateItemAsync(_cardsViewModel);
            //this.CloseItemAsync(this);
        }
    }
}

