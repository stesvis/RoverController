using Microsoft.AspNet.Identity;
using RoverController.Lib;
using RoverController.Logger;
using RoverController.Web.DTOs;
using RoverController.Web.Helper;
using RoverController.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

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

        [HttpPost]
        [Route("{id}/upload")]
        public async Task<IHttpActionResult> Upload(int id)
        {
            var identity = RequestContext.Principal.Identity;
            var userId = identity.GetUserId();

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new CustomMultipartFormDataStreamProvider(root);

            try
            {
#if false
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return result;
#endif
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                var file = provider.FileData.FirstOrDefault();
                if (file == null)
                {
                    return BadRequest("Cannot fetch the uploaded file.");
                }

                // This illustrates how to get the file names.
                var files = new List<string>
                {
                    Path.GetFileName(file.LocalFileName)
                };
                System.Diagnostics.Debug.WriteLine(file.Headers.ContentDisposition.FileName);
                System.Diagnostics.Debug.WriteLine("Server file path: " + file.LocalFileName);

                var attachmentDTO = new MissionAttachmentDTO
                {
                    MissionId = id,
                    OriginalFilename = Path.GetFileName(file.LocalFileName),
                    FileType = Path.GetExtension(file.LocalFileName),
                    FileName = Path.Combine(root, file.LocalFileName),
                    AWSPublicUrl = Path.Combine(Api.ApiBaseUrl, $@"App_Data/{Path.GetFileName(file.LocalFileName)}")
                };

                attachmentDTO = AppService.Missions.Attach(id, attachmentDTO, userId);

                return Ok(attachmentDTO);
            }
            catch (Exception ex)
            {
                AppLogger.Logger.Error(ex);
                return InternalServerError(ex);
            }
        }
    }

    // We implement MultipartFormDataStreamProvider to override the filename of File which will be
    // stored on server, or else the default name will be of the format like Body- Part_{GUID}. In
    // the following implementation we simply get the FileName from ContentDisposition Header of the
    // Request Body.
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            return $"{Guid.NewGuid()}-{headers.ContentDisposition.FileName.Replace("\"", string.Empty)}";
        }
    }
}