using ApiDataAccess.Library.Helpers;
using Caliburn.Micro;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive // everything can go into the conductor 
    {

        private readonly MenuViewModel menuViewModel;
        private readonly PopUpViewModel popUpViewModel;

        public ShellViewModel(MenuViewModel menuViewModel, PopUpViewModel popUpViewModel)
        {
            ApiHelper.InitializeClient();

            this.menuViewModel = menuViewModel;
            this.popUpViewModel = popUpViewModel;
        }

        public async Task OnViewLoaded()
        {
            await ActivateItemAsync(popUpViewModel);

            await menuViewModel.UpdateDatabase();
            await menuViewModel.cardsViewModel.LoadDataAsync();
            await menuViewModel.myCollectionViewModel.LoadCollectionAsync();

            await ActivateItemAsync(menuViewModel);
            await popUpViewModel.Close();
            
        }
    }
}

