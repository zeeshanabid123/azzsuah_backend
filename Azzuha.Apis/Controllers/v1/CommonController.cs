using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.Apis.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azzuha.Apis.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService commonService;
        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<List<General<int>>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getgenders")]
        public IActionResult GetGenders()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<General<int>>>() { isError = false, messages = "", data = commonService.GetGenders() });
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
        [ProducesResponseType(typeof(Response<List<General<int>>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getcountries")]
        public IActionResult GetCountries()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<General<int>>>() { isError = false, messages = "", data = commonService.GetCountries() });
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
        [ProducesResponseType(typeof(Response<List<MediaTypeResponseModel>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getmediatypes")]
        public IActionResult GetMediaTypes()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<MediaTypeResponseModel>>() { isError = false, messages = "", data = commonService.GetMediaTypes() });
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
        [ProducesResponseType(typeof(Response<List<AdminSliderResponse>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getslider")]
        public IActionResult GetSlider()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<AdminSliderResponse>>() { isError = false, messages = "", data = commonService.GetSlider() });
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
        [ProducesResponseType(typeof(Response<List<General<int>>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getstates/{countryId:int}")]
        public IActionResult GetStates(int countryId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<General<int>>>() { isError = false, messages = "", data = commonService.GetStates(countryId) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<bool>), 200)]
        [ProducesResponseType(typeof(Response<bool>), 403)]
        [HttpGet]
        [CustomAuthorize(false)]
        [Route("isemailexists/{email}")]
        public IActionResult IsEmailExists(string email)
        {
            try
            {
                Guid userId = Guid.Parse(RouteData.Values["userId"].ToString());
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = commonService.IsEmailExists(email) });
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
        [ProducesResponseType(typeof(Response<List<General<int>>>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getcities/{stateId:int}")]
        public IActionResult GetCities(int stateId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<General<int>>>() { isError = false, messages = "", data = commonService.GetCities(stateId) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        /// <summary>
        /// Get All Content from this Api
        /// </summary>
        /// <param name="contentId">1. for Terms And Condition
        /// 2. for privacy policy</param>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getcontent/{contentId}")]
        public IActionResult GetContent(string contentId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<string>() { isError = false, messages = "", data = commonService.GetContent(Guid.Parse(contentId)) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Get All Content from this Api
        /// </summary>
        /// <param name="Id">1. for Terms And Condition
        /// 2. for privacy policy</param>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<JsonResponseModel>), 200)]
        [CustomAuthorize(false)]
        [HttpGet]
        [Route("getjsoncontent/{Id}")]
        public IActionResult GetJsonContent(string Id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<JsonResponseModel>() { isError = false, messages = "", data = commonService.GetJsonData(Id) });
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
        [Route("savecontactquery")]
        public async Task<IActionResult> SaveContactQuery(QueryRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                }

                Guid userId = SystemGlobal.GetId();
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await commonService.SaveContactQuery(request, userId) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    

    }
}