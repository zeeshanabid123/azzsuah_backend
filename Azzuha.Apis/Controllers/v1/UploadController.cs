using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.Apis.Models;
using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Azzuha.Apis.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration configuration;

        public UploadController(IWebHostEnvironment _env, IConfiguration configuration)
        {
            this._env = _env;
            this.configuration = configuration;
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="403">If client made some mistake</response>  
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<List<FileUrlResponce>>), 200)]
        [ProducesResponseType(typeof(Response<List<FileUrlResponce>>), 403)]
        [CustomAuthorize(false)]
        [HttpPost]
        [Route("uploadmedia")]
        public IActionResult UploadMedia([FromForm]UploadDocuments param, IFormFile mediaFile)
        {
            try
            {
                var response = new List<FileUrlResponce>();
                FileUrlResponce vedioFile = new FileUrlResponce();

                var fileName = mediaFile.FileName;
                if (param.Type == (int)EDocumentType.Image)
                {
                    var file = new SaveFiles().SaveImage(_env.WebRootPath, mediaFile, "Images", "Images");

                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                }
                else if (param.Type == (int)EDocumentType.Video)
                {
                    vedioFile = new SaveFiles().SaveFile(_env.WebRootPath, mediaFile, "video");

                    if (!string.IsNullOrEmpty(vedioFile.URL) && !string.IsNullOrEmpty(vedioFile.ThumbUrl))
                    {
                        response.Add(vedioFile);
                    }
                }
                else if (param.Type == (int)EDocumentType.Document)
                {
                    var file = new SaveFiles().SaveFile(_env.WebRootPath, mediaFile, "Document");

                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                }

                if (response.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new Response<List<FileUrlResponce>>() { isError = false, messages = "", data = response });
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new Response<List<FileUrlResponce>>() { isError = true, messages = Error.FileNotFound, data = new List<FileUrlResponce>() });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// </summary>
        /// <response code="200">If all working fine</response>
        /// <response code="403">If client made some mistake</response>  
        /// <response code="500">If Server Error</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(Response<List<FileUrlResponce>>), 200)]
        [ProducesResponseType(typeof(Response<List<FileUrlResponce>>), 403)]
        [CustomAuthorize(false)]
        [HttpPost]
        [Route("uploadmedias3")]
        public async Task<IActionResult> UploadMediaS3([FromForm]UploadDocuments param, IFormFile mediaFile)
        {
            try
            {
                var response = new List<FileUrlResponce>();
                FileUrlResponce vedioFile = new FileUrlResponce();
                var subBucketName = DateTime.UtcNow.ToString("yyyy/MMM/dd");

                var fileName = mediaFile.FileName;
                if (param.Type == (int)EDocumentType.Image)
                {
                    var file = await new SaveFiles().SendMyFileToS3(mediaFile, configuration.GetValue<string>("Amazon:Bucket"), "media/" + subBucketName, false, configuration.GetValue<string>("Amazon:AccessKey"), configuration.GetValue<string>("Amazon:AccessSecret"), configuration.GetValue<string>("Amazon:BaseUrl"));

                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                }
                else if (param.Type == (int)EDocumentType.Video)
                {
                    vedioFile = await new SaveFiles().SendMyFileToS3(mediaFile, configuration.GetValue<string>("Amazon:Bucket"), "video/" + subBucketName, true, configuration.GetValue<string>("Amazon:AccessKey"), configuration.GetValue<string>("Amazon:AccessSecret"), configuration.GetValue<string>("Amazon:BaseUrl"));

                    if (!string.IsNullOrEmpty(vedioFile.URL) && !string.IsNullOrEmpty(vedioFile.ThumbUrl))
                    {
                        response.Add(vedioFile);
                    }
                }
                else if (param.Type == (int)EDocumentType.Document)
                {
                    var file = await new SaveFiles().SendMyFileToS3(mediaFile, configuration.GetValue<string>("Amazon:Bucket"), "document/" + subBucketName, true, configuration.GetValue<string>("Amazon:AccessKey"), configuration.GetValue<string>("Amazon:AccessSecret"), configuration.GetValue<string>("Amazon:BaseUrl"));

                    if (!string.IsNullOrEmpty(file.URL))
                    {
                        file.URL = file.URL;
                        response.Add(file);
                    }
                }

                if (response.Any())
                {
                    return StatusCode(StatusCodes.Status200OK, new Response<List<FileUrlResponce>>() { isError = false, messages = "", data = response });
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new Response<List<FileUrlResponce>>() { isError = true, messages = Error.FileNotFound, data = new List<FileUrlResponce>() });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}