using ApiDataAccess.Library.Helpers;
using Caliburn.Micro;
using Logger.Library;
using System.Threading.Tasks;
using WpfDesktopUI.Library;

namespace WpfDesktopUI.ViewModels
{
    public class MenuViewModel : Conductor<object>.Collection.OneActive // everything can go into the conductor 
    {

        protected internal readonly CardsViewModel cardsViewModel;
        protected internal readonly MyCollectionViewModel myCollectionViewModel;

        public MenuViewModel(CardsViewModel cardsViewModel, MyCollectionViewModel myCollectionViewModel)
        {
            ApiHelper.InitializeClient();

            this.cardsViewModel = cardsViewModel;
            this.myCollectionViewModel = myCollectionViewModel;
        }

        public async Task UpdateDatabase()
        {
            await Mapper.UpdateDatabase();
            await Log.Info("Database has been updated");
        }

        public void LoadCollection()
        {
            ActivateItemAsync(myCollectionViewModel);

            // this.CloseItemAsync(this);
        }

        public void LoadCards()
        {
            ActivateItemAsync(cardsViewModel);

            // this.CloseItemAsync(this);
        }

    }
}