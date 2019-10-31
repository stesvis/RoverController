using System.Collections.Generic;

namespace RoverController.Web.ViewModels
{
    public abstract class BaseViewModel
    {
        public IList<string> Errors { get; set; }

        public string UserHighestRole { get; set; }

        public BaseViewModel()
        {
            Errors = new List<string>();
        }
    }
}