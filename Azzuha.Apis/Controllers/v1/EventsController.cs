using Azzuha.Apis.Models;
using Azzuha.DataViewModels.Common;
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
    public class EventsController : ControllerBase
    {

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<List<GalleryResponseModel>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getevent")]
        public IActionResult GetEventData()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<GalleryResponseModel>>() { isError = false, messages = "", data = galleryService.GetGallery() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

   
}
