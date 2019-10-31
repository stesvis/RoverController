using Microsoft.AspNet.Identity.EntityFramework;

namespace RoverController.Web.Models
{
    public class ApplicationRole : IdentityRole

    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string name) : base(name)
        {
        }

        public ApplicationRole(string name, int order) : base(name)
        {
            this.Order = order;
        }

        public virtual int Order { get; set; }
    }
}