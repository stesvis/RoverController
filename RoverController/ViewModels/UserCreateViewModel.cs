using Microsoft.AspNet.Identity;

namespace RoverController.Web.ViewModels
{
    public class UserCreateViewModel
    {
        public string UserId { get; set; }
        public IdentityResult Result { get; set; }
    }
}