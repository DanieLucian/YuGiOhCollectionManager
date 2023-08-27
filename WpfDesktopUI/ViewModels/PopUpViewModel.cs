using Caliburn.Micro;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WpfDesktopUI.ViewModels
{
    public class PopUpViewModel : Screen
    {
        public async Task Close()
        {
            await TryCloseAsync();
        }
    }
}