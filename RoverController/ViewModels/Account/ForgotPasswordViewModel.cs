using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        //[EmailAddress]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}