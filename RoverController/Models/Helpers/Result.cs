using System.Collections.Generic;

namespace RoverController.Web.Models.Helpers
{
    public class Result
    {
        /// <summary>
        /// Whether the operation was successful or not
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        /// The saved object
        /// </summary>
        public object Entity { get; set; }
    }
}