using Microsoft.AspNet.Identity;
using RoverController.Logger;
using RoverController.Web.Services;
using System;
using System.Web.Http;
using System.Web.WebPages;

namespace RoverController.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Positions")]
    public class PositionsController : BaseApiController
    {
        public PositionsController(IAppService appService) : base(appService)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult All(int? clientId)
        {
            try
            {
                var identity = RequestContext.Principal.Identity;
                var userId = identity.GetUserId();

                if (userId.IsEmpty())
                {
                    return Unauthorized();
                }

                var productDTOs = AppService.Positions.All();

                return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                return InternalServerError(ex);
            }
        }
    }
}