using Microsoft.AspNet.Identity;

namespace RoverController.Web.Models.Helpers
{
    public class UserResult
    {
        public ApplicationUser User { get; set; }

        public IdentityResult Result { get; set; }
    }
}