using Microsoft.AspNet.Identity;
using RoverController.Logger;
using RoverController.Web.Services;
using System;
using System.Web.Http;
using System.Web.WebPages;

namespace RoverController.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/PinPoints")]
    public class PinPointsController : BaseApiController
    {
        public PinPointsController(IAppService appService) : base(appService)
        {
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult All()
        {
            try
            {
                var identity = RequestContext.Principal.Identity;
                var userId = identity.GetUserId();

                if (userId.IsEmpty())
                {
                    return Unauthorized();
                }

                var productDTOs = AppService.PinPoints.All();

                return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Retusn one pin-point by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var identity = RequestContext.Principal.Identity;
                var userId = identity.GetUserId();
                var pinPointDTO = AppService.PinPoints.Get(id);

                // invalid user
                if (userId.IsEmpty())
                {
                    return Unauthorized();
                }

                // no results
                if (pinPointDTO == null)
                {
                    return NotFound();
                }

                return Ok(pinPointDTO);
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                return InternalServerError(ex);
            }
        }
    }
}