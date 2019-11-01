using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoverController.Web.Helper
{
    public static class WebExtensions
    {
        public static IEnumerable<string> GetErrors(this System.Web.Mvc.ModelStateDictionary modelState)
        {
            //return modelState.Values.SelectMany(v => v.Errors)
            //                        .Select(v => v.ErrorMessage + " " + v.Exception).ToList();
            var errorsList = modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage/* + " " + x.Exception*/);
            return errorsList;
        }

        public static IEnumerable<string> GetErrors(this System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            //return modelState.Values.SelectMany(v => v.Errors)
            //                        .Select(v => v.ErrorMessage + " " + v.Exception).ToList();
            var errorsList = modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage/* + " " + x.Exception*/);
            return errorsList;
        }
    }
}