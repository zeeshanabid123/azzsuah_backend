using Azzuha.Apis.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azzuha.Apis.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : Controller
    {
        private readonly IMediaService mediaService;
        public MediaController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<List<MediaResponseModel>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getMedia/{Id}")]
        public IActionResult GetMedia(string Id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<MediaResponseModel>>() { isError = false, messages = "", data = mediaService.GetMediaById(new Guid(Id)) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

