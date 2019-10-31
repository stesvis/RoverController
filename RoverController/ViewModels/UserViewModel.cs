using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RoverController.Web.DTOs;

namespace RoverController.Web.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Username (*)")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email (*)")]
        public string Email { get; set; }

        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        //[RequiredIf("string.IsNullOrWhiteSpace(Id)", AllowEmptyStrings = false, ErrorMessage = "The Password field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name (*)")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name (*)")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Display(Name = "Phone # (*)")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Role")]
        public string RoleId { get; set; }

        [Required]
        [Display(Name = "Company Code (*)")]
        public Guid? ClientGuid { get; set; }

        //[Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        // Used for UserDTO -> UserViewModel mapping
        [Display(Name = "Main Branch")]
        public int PrimaryClientId { get; set; }

        // Used to determine to which client we are adding a new user
        public int ClientId { get; set; }

        public IList<string> Roles { get; set; }

        public IList<RoleDTO> RoleCheckboxes { get; set; }

        public IList<RoleDTO> UserRoles { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public UserViewModel()
        {
            Id = Guid.NewGuid().ToString();
            Roles = new List<string>();
            RoleCheckboxes = new List<RoleDTO>();
            UserRoles = new List<RoleDTO>();
        }
    }
}