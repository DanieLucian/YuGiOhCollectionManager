using ApiDataAccess.Library.Helpers;
using Caliburn.Micro;
using Logger.Library;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfDesktopUI.Library;

namespace WpfDesktopUI.ViewModels
{
    public class MenuViewModel : Conductor<object>.Collection.OneActive // everything can go into the conductor 
    {

        protected internal readonly CardsViewModel cardsViewModel;
        private readonly CollectionViewModel collectionViewModel;

        public MenuViewModel(CardsViewModel cardsViewModel, CollectionViewModel collectionViewModel)
        {
            ApiHelper.InitializeClient();

            this.cardsViewModel = cardsViewModel;
            this.collectionViewModel = collectionViewModel;
        }

        public async Task UpdateDatabase()
        {
            await Mapper.UpdateDatabase();
            await Log.Info("Database has been updated");
        }

        public void LoadCollection()
        {
            ActivateItemAsync(collectionViewModel);

            // this.CloseItemAsync(this);
        }

        public void LoadCards()
        {
            ActivateItemAsync(cardsViewModel);

            // this.CloseItemAsync(this);
        }

    }
}