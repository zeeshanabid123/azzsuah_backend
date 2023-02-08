using Azzuha.AdminPanel.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azzuha.AdminPanel.Controllers
{
    public class JsonEditorController : Controller
    {

                private readonly IAdminJsonEditorService adminJsonEditorService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;
        private readonly IAdminAccountService adminAccountService;

        public JsonEditorController(IAdminJsonEditorService adminJsonEditorService, IAdminAccountService adminAccountService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.adminJsonEditorService = adminJsonEditorService;
            this._env = _env;
            this.configuration = configuration;
            this.adminAccountService = adminAccountService;
        }
        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> SaveBlogs(AdminJsonEditorRequest save)
        {
            bool response1 = false;
            if (save.Id != null && save.Id != Guid.Empty)
            {

                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminJsonEditorService.EditJsonData(save)
                });

            }
            if (save.Id == Guid.Empty)
            {

                save.Id = SystemGlobal.GetId();

                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminJsonEditorService.saveJsonData(save, new IdentityHelper(adminAccountService).GetUserId(User.Identity.Name))
                });

            }
            return Ok(response1);
        }

        public IActionResult EditJsonData(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AdminJsonEditorRequest>() { isError = false, messages = "", data = adminJsonEditorService.GetJsonDataById(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
