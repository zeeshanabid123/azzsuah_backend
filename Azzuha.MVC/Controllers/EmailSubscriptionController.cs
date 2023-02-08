using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests;
using Azzuha.Services.Interface.admin;

namespace Azzuha.AdminPanel.Controllers
{
    public class EmailSubscriptionController : Controller
    {
        private readonly IEmailSubscriptionAdminService _emailSubscriptionAdminService;

        public EmailSubscriptionController(IEmailSubscriptionAdminService emailSubscriptionAdminService)
        {
            this._emailSubscriptionAdminService = emailSubscriptionAdminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get(GenralReportListRequest<EmailSubscriptionSaveResponse> model, DateTime? str, DateTime? endd)
        {
            return Json(new { data = _emailSubscriptionAdminService.Get(model, str, endd) });
        }
    }
}
