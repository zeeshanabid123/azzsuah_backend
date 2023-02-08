using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.AdminPanel.Models;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Azzuha.AdminPanel.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        private readonly IVoucherService voucherService;
        private readonly IAdminAccountService adminAccountService;

        public VoucherController(IVoucherService voucherService, IAdminAccountService adminAccountService)
        {
            this.voucherService = voucherService;
            this.adminAccountService = adminAccountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetVouchers(GenralReportListRequest<GetVoucherResponse> model, DateTime? str, DateTime? endd)
        {
            return Json(new { data = voucherService.GetVouchers(model, str, endd) });
        }

        public async Task<JsonResult> Create(CreateVoucherRequest request)
        {
            bool response = await voucherService.Save(request, new IdentityHelper(adminAccountService).GetUserId(User.Identity.Name));
            return Json(new { data = response });
        }

        public async Task<JsonResult> ControlActivation(Guid id, bool isActive)
        {
            return Json(new { data = await voucherService.ControlActivation(id, isActive, new IdentityHelper(adminAccountService).GetUserId(User.Identity.Name)) });
        }

        public JsonResult IsCodeExist(string code)
        {
            return Json(new { data = voucherService.IsCodeExist(code) });
        }

        public IActionResult EditVochers(Guid id)
        {
            try
            {
                return Json(new { data = voucherService.EditVochers(id) });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}