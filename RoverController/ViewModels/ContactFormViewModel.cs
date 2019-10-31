using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.ViewModels
{
    public class ContactFormViewModel
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public int ClientId { get; set; }

        public string Username { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public bool Success { get; set; }

        public string ResultMessage { get; set; }
    }
}