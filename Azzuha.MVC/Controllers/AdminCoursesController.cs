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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Azzuha.AdminPanel.Controllers
{
    public class AdminCoursesController : Controller
    {
        private readonly IAdminCoursesService adminCoursesService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;

        public AdminCoursesController(IAdminCoursesService AdminCoursesService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.adminCoursesService = AdminCoursesService;
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
                    return StatusCode(StatusCodes.Status200OK, new Response<List<AdminCourseResponse>>() { isError = false, messages = "", data = adminCoursesService.GetCourses(SearchFilter, skip, take) });
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

        public async Task<IActionResult> SaveBlogs(AdminCourseRequest save, IFormFile base64)
        {
            bool response1 = false;
            if (save.Id != null && save.Id != Guid.Empty)
            {
            
                if (base64 != null)
                {
                    var response = new List<FileUrlResponce>();
                    var file = new SaveFiles().SaveImage(this._env.WebRootPath, base64,"courses", "coursesThubnail");
                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                    save.CourseImage = response.FirstOrDefault().URL;

                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminCoursesService.EditCourse(save)
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
                    save.CourseImage = response.FirstOrDefault().URL;

                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminCoursesService.SaveAdminCourse(save)
                });

            }
            return Ok(response1);
        }

        public IActionResult EditBlog(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AdminCourseResponse>() { isError = false, messages = "", data = adminCoursesService.GetCourseById(id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminCoursesService.PostSatus(flag, id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminCoursesService.DeleteCourse(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
