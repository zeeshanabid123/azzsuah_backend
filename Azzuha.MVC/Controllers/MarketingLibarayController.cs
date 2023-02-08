using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azzuha.AdminPanel.Controllers
{
    [Authorize]
    public class MarketingLibarayController : Controller
    {
        private readonly IAdminMarketingService _marketingservice;
       
        public MarketingLibarayController(IAdminMarketingService marketingservice)
        {
            this._marketingservice = marketingservice;
          
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProfileType()
        {
            return StatusCode(StatusCodes.Status200OK, new Response<List<ProfileTypeViewModel>>() { isError = false, messages = "", data = _marketingservice.GetProfileTypes() });
        }

        public IActionResult GeFreeClassesCate()
        {
            return StatusCode(StatusCodes.Status200OK, new Response<List<FreeClassCategoriesViewModel>>() { isError = false, messages = "", data = _marketingservice.GetFreeClasses() });
        }

        public JsonResult GetData(GenralReportListRequest<AdminMarketingResponse> page, DateTime? str, DateTime? endd, List<Guid> profileTypeId,int pId, bool Isused)
        {
            return Json(new { data = _marketingservice.GetList(page, str, endd, profileTypeId,pId,Isused) });
        }

        public IActionResult UpdateIsUsedStatus(Guid Id,int pid)
        {
            return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = _marketingservice.CheckIsUsedStatus(Id,pid) });
        }
    }
}
