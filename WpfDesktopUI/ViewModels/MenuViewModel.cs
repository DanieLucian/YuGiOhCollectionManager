using Caliburn.Micro;
using System.Threading.Tasks;
using ExternalServices;
using ApiDataAccess.Library.Helpers;

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