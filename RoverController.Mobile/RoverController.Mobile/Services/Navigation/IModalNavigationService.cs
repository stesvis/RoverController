using System.Threading.Tasks;

namespace RoverController.Mobile.Services.Navigation
{
    public interface IModalNavigationService
    {
        Task PopModalAsync(bool animated = true);
    }
}