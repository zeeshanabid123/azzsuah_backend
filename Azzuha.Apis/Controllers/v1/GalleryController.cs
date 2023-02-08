using Azzuha.Apis.Models;
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
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService galleryService;
        public GalleryController(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<List<GalleryResponseModel>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getGallery")]
        public IActionResult GetAboutusData()
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
