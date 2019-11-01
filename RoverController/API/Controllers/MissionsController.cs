using Microsoft.AspNet.Identity;
using RoverController.Logger;
using RoverController.Web.Services;
using System;
using System.Web.Http;
using System.Web.WebPages;

namespace RoverController.Web.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Missions")]
    public class MissionsController : BaseApiController
    {
        public MissionsController(IAppService appService) : base(appService)
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

                var productDTOs = AppService.Missions.All();

                return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Retusn one mission by id
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
                var missionDTO = AppService.Missions.Get(id);

                // invalid user
                if (userId.IsEmpty())
                {
                    return Unauthorized();
                }

                // no results
                if (missionDTO == null)
                {
                    return NotFound();
                }

                return Ok(missionDTO);
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                return InternalServerError(ex);
            }
        }
    }
}