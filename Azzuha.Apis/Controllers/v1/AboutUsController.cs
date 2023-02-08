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
    public class AboutUsController : ControllerBase
    {

        private readonly IAboutUsService aboutUsService;
        public AboutUsController(IAboutUsService aboutUsService)
        {
            this.aboutUsService = aboutUsService;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<AboutUsResponseModel>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getaboutus/{Id}")]
        public IActionResult GetAboutusData(string Id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AboutUsResponseModel>() { isError = false, messages = "", data = aboutUsService.GetAboutUsById(Id)});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
