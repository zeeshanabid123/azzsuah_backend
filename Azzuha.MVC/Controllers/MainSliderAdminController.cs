using Azzuha.AdminPanel.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response.AdminResponse;
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
    
   
    public class MainSliderAdminController : Controller
    {
        private readonly IAdminSliderService adminSliderService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;

        public MainSliderAdminController(IAdminSliderService AdminSliderService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.adminSliderService = AdminSliderService;
            this._env = _env;
            this.configuration = configuration;
        }
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
                    return StatusCode(StatusCodes.Status200OK, new Response<List<AdminSliderResponse>>() { isError = false, messages = "", data = adminSliderService.GetSlider(SearchFilter, skip, take) });
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

        public async Task<IActionResult> SaveBlogs(AdminSliderRequest save, IFormFile base64)
        {
            bool response1 = false;
            if (save.Id != null && save.Id != Guid.Empty)
            {

                if (base64 != null)
                {
                    var response = new List<FileUrlResponce>();
                    var file = new SaveFiles().SaveImage(this._env.WebRootPath, base64, "slider", "sliderThubnail");
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
                    data = await adminSliderService.EditSlider(save)
                });

            }
            if (save.Id == Guid.Empty)
            {

                save.Id = SystemGlobal.GetId();
                if (base64 != null)
                {
                    var response = new List<FileUrlResponce>();
                    var file = new SaveFiles().SaveImage(this._env.WebRootPath, base64, "courses", "coursesThubnail");
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
                    data = await adminSliderService.SaveAdminSlider(save)
                });

            }
            return Ok(response1);
        }

        public IActionResult EditBlog(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AdminSliderResponse>() { isError = false, messages = "", data = adminSliderService.GetSliderById(id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminSliderService.PostSatus(flag, id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminSliderService.DeleteSlider(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
