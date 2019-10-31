using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Membership Type")]
        public string RegisterType { get; set; }
    }
}