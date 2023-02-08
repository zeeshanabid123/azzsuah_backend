using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using CsvHelper;
using CsvHelper.Configuration;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Enum;
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
    public class ProfileController : Controller
    {
        private readonly IAdminListingService ListingService;

        public ProfileController(IAdminListingService ListingService)
        {
            this.ListingService = ListingService;
        }

        public IActionResult GetTrainerList()
        {
            return View();
        }

        public IActionResult GetAppList()
        {
            return View();
        }

        public IActionResult GetGymList()
        {
            return View();
        }

        public IActionResult GetClassList()
        {
            return View();
        }
        public IActionResult AllUsersList()
        {
            return View();
        }

        public IActionResult BusinessFitnessNeutrationAppList()
        {
            return View();
        }

        public IActionResult GetCustomerList()
        {
            return View();
        }

        public JsonResult GetData(GenralReportListRequest<ListResponse> page, DateTime? str, DateTime? endd, Guid profileTypeId)
        {
            return Json(new { data = ListingService.GetList(page, str, endd, profileTypeId) });
        }

        public JsonResult GetUsersData(GenralReportListRequest<AllUsersResponse> page, DateTime? str, DateTime? endd, bool? status)
        {
            return Json(new { data = ListingService.GetAllUsersReports(page, str, endd, status) });
        }


        [HttpGet]
        public ActionResult Export(Guid profileTypeId)
        {
            GenralReportListRequest<ListResponse> page = new GenralReportListRequest<ListResponse>();
            page.Page = 0;
            page.PageSize = 100000000;
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            var list = ListingService.GetList(page, DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddDays(10), profileTypeId);

            DataTable Dt = new DataTable();
            Dt.Columns.Add("Date", typeof(string));
            Dt.Columns.Add("Trainers", typeof(string));
            Dt.Columns.Add("Location", typeof(string));
            if (profileTypeId.ToString().ToLower().Equals(EProfileType.FitnessTrainner.ToLower()))
            {
                Dt.Columns.Add("Training Facility", typeof(string));
                Dt.Columns.Add("Categories", typeof(string));
            }
            else if (profileTypeId.ToString().ToLower().Equals(EProfileType.FitnessApp.ToLower()) || profileTypeId.ToString().ToLower().Equals(EProfileType.BusinessFitnessClass.ToLower()))
            {
                Dt.Columns.Add("Services", typeof(string));
                Dt.Columns.Add("Categories", typeof(string));
            }
            else if (profileTypeId.ToString().ToLower().Equals(EProfileType.BusinessFitnessNeutrationApp.ToLower()))
            {
                Dt.Columns.Add("Services", typeof(string));
            }
            else if (profileTypeId.ToString().ToLower().Equals(EProfileType.Customers.ToLower()))
            {
                Dt.Columns.Add("Customer Channels", typeof(string));
                Dt.Columns.Add("Categories", typeof(string));
            }
            Dt.Columns.Add("Average Hourly Price", typeof(string));
            Dt.Columns.Add("Number Of Marketing Materials", typeof(string));
            Dt.Columns.Add("Number Of Views", typeof(string));
            Dt.Columns.Add("Number Of Messages", typeof(string));
            Dt.Columns.Add("Number Of Likes", typeof(string));
            Dt.Columns.Add("Fee", typeof(string));
            Dt.Columns.Add("Subscription Term", typeof(string));


            foreach (var data in list.Entities)
            {
                int count = 0;
                DataRow row = Dt.NewRow();
                row[count++] = data.Date.ToString("dd/MMMM/yyyy hh:mm:ss");
                row[count++] = data.Name;
                row[count++] = string.Join(" | ", data.Location);
                if (!profileTypeId.ToString().ToLower().Equals(EProfileType.BusinessFitnessGym.ToLower()))
                {
                    row[count++] = string.Join(',', data.List1);
                    if (!profileTypeId.ToString().ToLower().Equals(EProfileType.BusinessFitnessNeutrationApp.ToLower()))
                    {
                        row[count++] = string.Join(',', data.List2);
                    }
                }
                row[count++] = data.AverageHourlyPrice;
                row[count++] = data.NumberOfMarketingMaterials;
                row[count++] = data.NumberOfViews;
                row[count++] = data.NumberOfMessages;
                row[count++] = data.NumberOfLikes;
                row[count++] = data.Fee;
                row[count++] = data.SubscriptionTerm;

                Dt.Rows.Add(row);

            }

            var memoryStream = new MemoryStream();
            using (var excelPackage = new ExcelPackage(memoryStream))
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.Light21);
                worksheet.Cells["A1:N1"].Style.Font.Bold = true;

                SetBorder(worksheet.Cells["A1:N1"]);
                SetBoldAndSize(worksheet.Cells["A1:N1"]);
                SetWrapTextAndAutofit(worksheet.Cells["A1:N1"]);
                worksheet.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                worksheet.DefaultRowHeight = 18;

                worksheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.DefaultColWidth = 20;
                worksheet.Column(2).AutoFit();

                var fileName = "export_" + (profileTypeId == Guid.Parse(EProfileType.FitnessTrainner) ? "Fitness_Trainer"
                   : profileTypeId == Guid.Parse(EProfileType.FitnessApp) ? "Fitness_Apps"
                   : profileTypeId == Guid.Parse(EProfileType.BusinessFitnessClass) ? "Classes_& Studios"
                   : profileTypeId == Guid.Parse(EProfileType.BusinessFitnessGym) ? "Gym"
                   : profileTypeId == Guid.Parse(EProfileType.BusinessFitnessNeutrationApp) ? "Health_&_Nutrition_Apps"
                   : profileTypeId == Guid.Parse(EProfileType.Customers) ? "Customers"
                   : "") + $"{DateTime.UtcNow.Ticks}.csv";

                return File(excelPackage.GetAsByteArray(), "application/octet-stream", fileName);
            }
        }


        [HttpGet]
        public ActionResult ExportUsers(bool status)
        {
            GenralReportListRequest<AllUsersResponse> page = new GenralReportListRequest<AllUsersResponse>();
            page.Page = 0;
            page.PageSize = 100000000;
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            var list = ListingService.GetAllUsersReports(page, DateTime.UtcNow.AddYears(-10), DateTime.UtcNow.AddDays(10), status);

            DataTable Dt = new DataTable();
            Dt.Columns.Add("Name", typeof(string));
            Dt.Columns.Add("ProfileTypeName", typeof(string));
            Dt.Columns.Add("Email", typeof(string));
          
            Dt.Columns.Add("SignUpdate", typeof(string));
            Dt.Columns.Add("Status", typeof(string));
            foreach (var data in list.Entities)
            {
                int count = 0;
                DataRow row = Dt.NewRow();
                row[count++] = data.Name;
                row[count++] = data.ProfileTypeName;
                row[count++] = data.Email;
                row[count++] = data.SignUpdate.ToString("dd/MMMM/yyyy hh:mm:ss");
                row[count++] = data.Status;
                Dt.Rows.Add(row);

            }

            var memoryStream = new MemoryStream();
            using (var excelPackage = new ExcelPackage(memoryStream))
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.Light21);
                worksheet.Cells["A1:N1"].Style.Font.Bold = true;

                SetBorder(worksheet.Cells["A1:N1"]);
                SetBoldAndSize(worksheet.Cells["A1:N1"]);
                SetWrapTextAndAutofit(worksheet.Cells["A1:N1"]);
                worksheet.Cells["A1:N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                worksheet.DefaultRowHeight = 18;

                worksheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.DefaultColWidth = 20;
                worksheet.Column(2).AutoFit();

                var fileName = "export_"  + $"{DateTime.UtcNow.Ticks}.csv";

                return File(excelPackage.GetAsByteArray(), "application/octet-stream", fileName);
            }
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