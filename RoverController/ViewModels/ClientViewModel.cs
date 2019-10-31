using System;
using System.ComponentModel.DataAnnotations;

namespace RoverController.Web.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Company", ShortName = "Company")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(50)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "Province/State")]
        [MaxLength(50)]
        public string Province { get; set; }

        [Display(Name = "Postal/Zip")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", NullDisplayText = "Never")]
        [Display(Name = "Expiry Date", ShortName = "Expiration")]
        public DateTime? ExpiryDate { get; set; }

        public bool HasDoneImport { get; set; }

        public string TempGuid { get; set; }

        [Display(Name = "Auto Renew", ShortName = "Renew")]
        public bool AutoRenew { get; set; }

        [Display(Name = "Is Demo", ShortName = "Demo")]
        public bool IsDemo { get; set; }

        [Display(Name = "Price per User", ShortName = "Fee")]
        public double Fee { get; set; }
    }
}