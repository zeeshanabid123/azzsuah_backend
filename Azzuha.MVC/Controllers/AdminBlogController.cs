using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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

namespace Azzuha.AdminPanel.Controllers
{
    [Produces("application/json")]
 
    public class AdminBlogController : Controller
    {
        private readonly IBlogAdminService blogAdminService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;

        public AdminBlogController(IBlogAdminService BlogAdminService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.blogAdminService = BlogAdminService;
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
                    return StatusCode(StatusCodes.Status200OK, new Response<List<AdminBlogResponse>>() { isError = false, messages = "", data = blogAdminService.GetBlogs(SearchFilter, skip, take) });
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

        public async Task<IActionResult> SaveBlogs(BlogAdminRequest save, IFormFile base64)
        {
            bool response1 = false;
            if (save.Id != null && save.Id != Guid.Empty)
            {
                DateTime oDate = DateTime.Parse(save.Date, CultureInfo.CreateSpecificCulture("en-US"));
                save.PublishDate = oDate;
                string slugurl = StringHelper.UrlFriendly(save.Title, int.MaxValue);
                save.slugUrl = slugurl;
                if (base64 != null)
                {
                    var response = new List<FileUrlResponce>();
                    var file = await new SaveFiles().SendMyFileToS3(base64, configuration.GetValue<string>("Amazon:Bucket"), "BlogImages", false, configuration.GetValue<string>("Amazon:AccessKey"), configuration.GetValue<string>("Amazon:AccessSecret"), configuration.GetValue<string>("Amazon:BaseUrl"));

                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                    save.ImageUrl = response.FirstOrDefault().URL;

                    save.ImageThumbnailUrl = response.FirstOrDefault().ThumbUrl;
                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await blogAdminService.EditBlog(save)
                });

            }
            if (save.Id == Guid.Empty)
            {

                save.Id = SystemGlobal.GetId();
                DateTime oDate = DateTime.Parse(save.Date, CultureInfo.CreateSpecificCulture("en-US"));
                save.PublishDate = oDate;
                string slugurl = StringHelper.UrlFriendly(save.Title, 12);
                save.slugUrl = slugurl;
                if (base64 != null)
                {
                    var response = new List<FileUrlResponce>();
                    var file = await new SaveFiles().SendMyFileToS3(base64, configuration.GetValue<string>("Amazon:Bucket"), "BlogImages", false, configuration.GetValue<string>("Amazon:AccessKey"), configuration.GetValue<string>("Amazon:AccessSecret"), configuration.GetValue<string>("Amazon:BaseUrl"));

                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                    save.ImageUrl = response.FirstOrDefault().URL;

                    save.ImageThumbnailUrl = response.FirstOrDefault().ThumbUrl;
                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await blogAdminService.SaveAdminBlog(save)
                });

            }
            return Ok(response1);
        }

        public IActionResult EditBlog(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AdminBlogResponse>() { isError = false, messages = "", data = blogAdminService.GetBlogById(id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await blogAdminService.PostSatus(flag, id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await blogAdminService.DeleteBlog(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}