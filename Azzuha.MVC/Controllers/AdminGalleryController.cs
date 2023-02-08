using Azzuha.AdminPanel.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests.AdminRequests;
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
    [Produces("application/json")]
    public class AdminGalleryController : Controller
    {
        private readonly IAdminGalleryService adminGalleryService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;

        public AdminGalleryController(IAdminGalleryService AdminGalleryService, IWebHostEnvironment _env, IConfiguration configuration)
        {
            this.adminGalleryService = AdminGalleryService;
            this._env = _env;
            this.configuration = configuration;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAdminGallery(string SearchFilter, int skip, int take)
        {
            try
            {
                try

                {

                    PaginationModel data = new PaginationModel();
                    return StatusCode(StatusCodes.Status200OK, new Response<List<AdminGalleryReponse>>() { isError = false, messages = "", data = adminGalleryService.GetGallery(SearchFilter, skip, take) });
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

        public async Task<IActionResult> SaveGallery(GalleryAdminRequest save, IEnumerable<IFormFile> files)
        {
            bool response1 = false;
            var response = new List<FileUrlResponce>();
            if (save.Id != null && save.Id != Guid.Empty)
            {
              
               
                if (files != null)
                {
                    response = new List<FileUrlResponce>();
                    save.GalleryImagesmodel = new List<AdminGalleryImageRequest>();
                    var file = new SaveFiles().SaveImages(this._env.WebRootPath, files);

                    if (file.Count() > 0)
                    {
                        foreach (var f in file)
                        {
                            f.URL = f.URL;
                            response.Add(f);
                            save.GalleryImagesmodel.Add(new AdminGalleryImageRequest
                            {
                                Id = SystemGlobal.GetId(),
                                GalleryImageUrl = f.ThumbUrl
                            });
                        }

                    }

                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminGalleryService.EditGallery(save)
                });

            }
            if (save.Id == Guid.Empty)
            {

                save.Id = SystemGlobal.GetId();
            
                if (files != null)
                {
                     response = new List<FileUrlResponce>();
                    save.GalleryImagesmodel = new List<AdminGalleryImageRequest>();
                    var baseUrl = configuration.GetValue<string>("BaseUrl");
                    var file =  new SaveFiles().SaveImages(this._env.WebRootPath, files);

                    if (file.Count() > 0)
                    {
                        foreach(var f in file)
                        {
                            f.URL = f.URL;
                            response.Add(f);
                            save.GalleryImagesmodel.Add(new AdminGalleryImageRequest
                            {
                                Id = SystemGlobal.GetId(),
                                GalleryImageUrl = f.ThumbUrl
                            });
                        }
                    
                    }
               
                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>()
                {
                    isError = false,
                    messages = "",
                    data = await adminGalleryService.SaveAdminGallery(save)
                });

            }
            return Ok(response1);
        }

        public IActionResult EditGallery(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<AdminGalleryReponse>() { isError = false, messages = "", data = adminGalleryService.GetGalleryById(id) });
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
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminGalleryService.PostSatus(flag, id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> DeleteGallery(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminGalleryService.DeleteGallery(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> DeleteGalleryImage(Guid id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = await adminGalleryService.DeleteGalleryImage(id) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
