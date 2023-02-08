using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests;
using Azzuha.Services.Interface.v1;

namespace Azzuha.Apis.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSubscriptionController : ControllerBase
    {
        private readonly IEmailSubscriptionService _emailSubscriptionService;

        public EmailSubscriptionController(IEmailSubscriptionService emailSubscriptionService)
        {
            this._emailSubscriptionService = emailSubscriptionService;
        }

        ///// <summary>
        ///// </summary>
        ///// <response code="200">If all working fine</response>
        ///// <response code="500">If Server Error</response>  
        //[ProducesResponseType(200)]
        //[ProducesResponseType(500)]
        //[ProducesResponseType(typeof(Response<bool>), 200)]
        //[HttpPost]
        //[Route("save")]
        //public async Task<IActionResult> Save(EmailSubscriptionSaveRequest save)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
        //        }

        //        return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await _emailSubscriptionService.Save(save) });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
