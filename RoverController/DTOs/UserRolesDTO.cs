using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RoverController.Web.DTOs
{
    public class IdentityUserRoleDTO
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
    }

    public class UserRoleDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class RoleDTO
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        public string Name { get; set; }

        public int Order { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }

    public class UserAndRolesDTO
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public List<UserRoleDTO> colUserRoleDTO { get; set; }
    }
}