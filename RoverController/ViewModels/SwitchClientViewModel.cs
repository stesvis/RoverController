using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.ViewModels
{
    public class SwitchClientViewModel
    {
        [Required(ErrorMessage = "Please select another company or branch.")]
        [Display(Name = "Switch to")]
        public int NextClientId { get; set; }
    }
}