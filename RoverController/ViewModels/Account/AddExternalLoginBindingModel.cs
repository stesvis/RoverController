using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.ViewModels.Account
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}