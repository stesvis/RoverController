using Microsoft.AspNet.Identity;
using RoverController.Logger;
using RoverController.Web.DTOs;
using RoverController.Web.Helper;
using RoverController.Web.Services;
using System;
using System.Linq;
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

                var missionDTOs = AppService.Missions.All();

                return Ok(missionDTOs);
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

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]MissionDTO missionDTO)
        {
            try
            {
                var identity = RequestContext.Principal.Identity;
                var userId = identity.GetUserId();

                // invalid user
                if (userId.IsEmpty())
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    var errors = string.Join($"{Environment.NewLine} - ", ModelState.GetErrors().ToArray());
                    AppLogger.Logger.Debug($"Create: Validation Error on new Mission by User {userId}{Environment.NewLine}{errors}");

                    return BadRequest(errors);
                }

                missionDTO.CreatedByUserId = userId;
                missionDTO.CreatedDate = DateTime.Now;

                missionDTO = AppService.Missions.Create(missionDTO, userId);

                return Created(new Uri($"{Request.RequestUri}/{missionDTO.Id}"), missionDTO);
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                return InternalServerError(ex);
            }
        }
    }
}