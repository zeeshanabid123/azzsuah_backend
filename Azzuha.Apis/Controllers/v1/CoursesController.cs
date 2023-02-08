using Azzuha.Apis.Models;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests;
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
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService coursesService;
        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<List<CoursesResponseModel>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getCourses")]
        public IActionResult GetAboutusData()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<CoursesResponseModel>>() { isError = false, messages = "", data = coursesService.GetCourses() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<bool>), 200)]
        [CustomAuthorize(false)]
        [HttpPost]
        [Route("saveadmission")]
        public async Task<IActionResult> Saveadmission(AdmissionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                }

                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await coursesService.SaveAmission(request) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
