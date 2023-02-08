using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Microsoft.AspNetCore.Http;
using Azzuha.DataViewModels.Common;
using Azzuha.Common;
using Azzuha.AdminPanel.Models;

namespace Azzuha.AdminPanel.Controllers
{
    [Produces("application/json")]
    public class AboutUsController : Controller
    {

        private readonly IAdminAboutUsService adminAboutUsService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;
        private readonly IAdminAccountService adminAccountService;

        public AboutUsController(IAdminAboutUsService adminAboutUsService, IAdminAccountService adminAccountService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.adminAboutUsService = adminAboutUsService;
            this._env = _env;
            this.configuration = configuration;
            this.adminAccountService = adminAccountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult ContactInfo()
        {
            return View();
        }

        public IActionResult BankDetail()
        {
            return View();
        }
        public async Task<IActionResult> SaveAboutUs(AboutUsAdminRequest save)
        {
            bool response1 = false;
            if (save.Id != null && save.Id != Guid.Empty)
            {
               
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminAboutUsService.EditAboutUs(save)
                });

            }
            if (save.Id == Guid.Empty)
            {

                save.Id = SystemGlobal.GetId();
              
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminAboutUsService.saveAbouts(save, new IdentityHelper(adminAccountService).GetUserId(User.Identity.Name))
                });

            }
            return Ok(response1);
        }

        public IActionResult EditAboutUs(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AboutUsAdminRequest>() { isError = false, messages = "", data = adminAboutUsService.GetAboutUsById(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



    }
}
