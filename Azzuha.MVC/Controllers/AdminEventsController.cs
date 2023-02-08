using Azzuha.AdminPanel.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
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
    public class AdminEventsController : Controller
    {
        private readonly IUpEventAdminService upEventAdminService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;

        public AdminEventsController(IUpEventAdminService UpEventAdminService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.upEventAdminService = UpEventAdminService;
            this._env = _env;
            this.configuration = configuration;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetBlogs(string SearchFilter, int skip, int take)
        {
            try
            {
                try

                {

                    PaginationModel data = new PaginationModel();
                    return StatusCode(StatusCodes.Status200OK, new Response<List<AdminEventResponse>>() { isError = false, messages = "", data = upEventAdminService.GetEvents(SearchFilter, skip, take) });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> SaveBlogs(EventAdminRequest save, IFormFile base64)
        {
            bool response1 = false;
            if (save.Id != null && save.Id != Guid.Empty)
            {

                if (base64 != null)
                {
                    var response = new List<FileUrlResponce>();
                    var file = new SaveFiles().SaveImage(this._env.WebRootPath, base64, "events", "eventsThubnail");
                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                    save.ImageUrl = response.FirstOrDefault().URL;

                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await upEventAdminService.EditEvents(save)
                });

            }
            if (save.Id == Guid.Empty)
            {

                save.Id = SystemGlobal.GetId();
                if (base64 != null)
                {
                    var response = new List<FileUrlResponce>();
                    var file = new SaveFiles().SaveImage(this._env.WebRootPath, base64, "events", "eventsThubnail");
                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                    save.ImageUrl = response.FirstOrDefault().URL;

                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await upEventAdminService.SaveAdminEvents(save)
                });

            }
            return Ok(response1);
        }

        public IActionResult EditBlog(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AdminEventResponse>() { isError = false, messages = "", data = upEventAdminService.GetEventById(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> PostStatus(int flag, Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await upEventAdminService.PostSatus(flag, id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await upEventAdminService.DeleteEvents(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult GetEventTypes()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<AdminEventTypeResponse>>() { isError = false, messages = "", data = upEventAdminService.GetEventsTypes() });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
