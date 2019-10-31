using System;
using System.Collections.Generic;
using System.Linq;

namespace RoverController.Web.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string RoleName
        {
            get
            {
                return Roles?.FirstOrDefault();
            }
        }

        public IList<string> Roles { get; set; }

        public IList<RoleDTO> UserRoles { get; set; }

        public string RoleNames
        {
            get
            {
                return string.Join(", ", Roles.ToArray());
            }
        }

        public DateTime? CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public string ShortName
        {
            get { return $"{FirstName} {LastName[0]}"; }
        }

        public UserDTO()
        {
            Roles = new List<string>();
            UserRoles = new List<RoleDTO>();
        }
    }
}