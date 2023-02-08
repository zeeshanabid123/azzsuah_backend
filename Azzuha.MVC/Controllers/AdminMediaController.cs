using Azzuha.AdminPanel.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response;
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
    public class AdminMediaController : Controller
    {
        private readonly IAdminMediaService adminMediaService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;

        public AdminMediaController(IAdminMediaService AdminMediaService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.adminMediaService = AdminMediaService;
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
                    return StatusCode(StatusCodes.Status200OK, new Response<List<AdminMediaResponse>>() { isError = false, messages = "", data = adminMediaService.GetMedia(SearchFilter, skip, take) });
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

        public async Task<IActionResult> SaveBlogs(AdminMediaRequest save, IFormFile file)
        {
            bool response1 = false;
            if (save.Id != null && save.Id != Guid.Empty)
            {

                if (file != null)
                {
                    var response = new List<FileUrlResponce>();
                    var savedfile = new SaveFiles().SaveFile(this._env.WebRootPath, file, "audio");
                    if (!string.IsNullOrEmpty(savedfile.URL))
                    {
                        savedfile.URL = savedfile.URL;
                        response.Add(savedfile);
                    }
                    save.FileUrl = response.FirstOrDefault().URL;

                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminMediaService.EditMedia(save)
                });

            }
            if (save.Id == Guid.Empty)
            {

                save.Id = SystemGlobal.GetId();
                if (file != null)
                {
                    var response = new List<FileUrlResponce>();
                    var savedfile = new SaveFiles().SaveFile(this._env.WebRootPath, file, "audio");
                    if (!string.IsNullOrEmpty(savedfile.URL))
                    {
                        savedfile.URL = savedfile.URL;
                        response.Add(savedfile);
                    }
                    save.FileUrl = response.FirstOrDefault().URL;

                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminMediaService.SaveAdminMedia(save)
                });

            }
            return Ok(response1);
        }

        public IActionResult EditBlog(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AdminMediaResponse>() { isError = false, messages = "", data = adminMediaService.GetMediaById(id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminMediaService.PostSatus(flag, id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminMediaService.DeleteMedia(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IActionResult GetMediaType()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<MediaTypeResponseModel>>() { isError = false, messages = "", data =  adminMediaService.GetMediaTypes() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
