using System.Threading.Tasks;

namespace RoverController.Mobile.Services.Navigation
{
    public class ModalNavigationService : IModalNavigationService
    {
        public async Task PopModalAsync(bool animated = true)
        {
            await App.Current.MainPage.Navigation.PopModalAsync(animated);
        }
    }
}