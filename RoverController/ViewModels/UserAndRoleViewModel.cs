using System.Collections.Generic;
using System.Web.Mvc;

namespace RoverController.Web.ViewModels
{
    public class UsersAndRole
    {
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<string> SelectedUsers { get; set; }

        public UsersAndRole()
        {
            Users = new List<SelectListItem>();
            SelectedUsers = new List<string>();
        }
    }
}