using Caliburn.Micro;
using System.Threading.Tasks;
using ExternalServices;
using ApiDataAccess.Library.Helpers;
using System;

namespace WpfDesktopUI.ViewModels
{
    public class MenuViewModel : Conductor<object>.Collection.OneActive // everything can go into the conductor 
    {

        private readonly IWindowManager windowManager;
        protected internal readonly CardsViewModel cardsViewModel;
        protected internal readonly MyCollectionViewModel myCollectionViewModel;
        protected internal readonly InsertMenuViewModel insertMenuViewModel;

        public MenuViewModel(CardsViewModel cardsViewModel, MyCollectionViewModel myCollectionViewModel, IWindowManager windowManager, InsertMenuViewModel insertMenuViewModel)
        {
            ApiHelper.InitializeClient();

            this.cardsViewModel = cardsViewModel;
            this.myCollectionViewModel = myCollectionViewModel;
            this.windowManager = windowManager;
            this.insertMenuViewModel = insertMenuViewModel;
        }

        public async Task UpdateDatabase()
        {
            await Mapper.UpdateDatabase();
        }

        public void LoadCollectionScreen()
        {
            ActivateItemAsync(myCollectionViewModel);

        }

        public void OpenInsertMenu()
        {
            ActivateItemAsync(insertMenuViewModel);
        }

        public void LoadCards()
        {
            if (!cardsViewModel.IsActive)
            {
                windowManager.ShowWindowAsync(cardsViewModel);
            }
        }

    }
}