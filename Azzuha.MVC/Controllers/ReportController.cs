using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace Azzuha.AdminPanel.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IAdminListingService listingService;

        public ReportController(IAdminListingService listingService)
        {
            this.listingService = listingService;
        }

        public IActionResult GetUnSubscribedList()
        {
            return View();
        }

        public IActionResult GetFeedbackList()
        {
            return View();
        }

        public IActionResult GetBlogFeedbackList()
        {
            return View();
        }

        public IActionResult GetClientMonthlyReports()
        {
            return View();
        }

        public IActionResult GetExternalMonthlyReports()
        {
            return View();
        }

        public IActionResult GetContactUsReports()
        {
            return View();
        }

        public IActionResult GetadmissionReports()
        {
            return View();
        }


        public JsonResult GetCntactUsData(GenralReportListRequest<ContactUsResponseModel> model, DateTime? str, DateTime? endd)
        {
            return Json(new { data = listingService.GetConatctusReports(model, str, endd) });
        }

        public JsonResult GetAdmintionDataData(GenralReportListRequest<AdminResponseViewModel> model, DateTime? str, DateTime? endd)
        {
            return Json(new { data = listingService.GetAdmintionReports(model, str, endd) });
        }





        private void SetCenter(ExcelRange range)
        {
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void SetLeftAlign(ExcelRange range)
        {
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        }

        private void SetBorder(ExcelRange range)
        {
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private void SetBoldAndSize(ExcelRange range, int size = 11)
        {
            range.Style.Font.Bold = true;
            range.Style.Font.Size = size;
        }

        private void SetWrapTextAndAutofit(ExcelRange range)
        {
            range.Style.WrapText = true;
        }
    }
}