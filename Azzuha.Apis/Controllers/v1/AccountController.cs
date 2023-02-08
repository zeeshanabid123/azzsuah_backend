using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azzuha.Apis.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<LoginResponse>), 200)]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequest login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                }

                var response = accountService.Login(login);

                if (response.Item1)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response<LoginResponse>() { isError = false, messages = "", data = response.Item2 });
                }

                return StatusCode(StatusCodes.Status400BadRequest, new Response<LoginResponse>() { isError = true, messages = Error.WrongEmail + " or " + Error.WrongPassword, data = new LoginResponse() });
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
        [HttpGet]
        [Route("forgotsentverificationlink/{email}")]
        public async Task<IActionResult> ForgotSentVerificationCode(string email)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await accountService.ForgotSentVerificationCode(email) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="400">If client made Validation Error</response>  
        /// <response code="403">If client made some mistake</response>  
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<bool>), 200)]
        [ProducesResponseType(typeof(Response<bool>), 403)]
        [ProducesResponseType(typeof(string[]), 400)]
        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState.Values.SelectMany(v => v.Errors.Select(z => z.ErrorMessage)));
                }

                bool res = await accountService.ChangePassword(changePassword);
                if (res)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = true });
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new Response<bool>() { isError = true, messages = Error.ForgotLinkIsExpired, data = false });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}