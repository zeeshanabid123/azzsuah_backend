using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Enum;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azzuha.AdminPanel.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IAdminAccountService adminAccount;
        public DashboardController(IAdminAccountService adminAccount)
        {
            this.adminAccount = adminAccount;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetCountTrainer()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<int>() { isError = false, messages = "", data = dashboardService.GetCounts(EProfileType.FitnessTrainner) });
        //}
        //public IActionResult GetCountApp()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<int>() { isError = false, messages = "", data = dashboardService.GetCounts(EProfileType.FitnessApp) });
        //}
        //public IActionResult GetCountGym()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<int>() { isError = false, messages = "", data = dashboardService.GetCounts(EProfileType.BusinessFitnessGym) });
        //}
        //public IActionResult GetCountNeutrationApp()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<int>() { isError = false, messages = "", data = dashboardService.GetCounts(EProfileType.BusinessFitnessNeutrationApp) });
        //}
        //public IActionResult GetCountClass()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<int>() { isError = false, messages = "", data = dashboardService.GetCounts(EProfileType.BusinessFitnessClass) });
        //}
        //public IActionResult GetCountCustomer()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<int>() { isError = false, messages = "", data = dashboardService.GetCounts(EProfileType.Customers) });
        //}

        ////Fee
        //public IActionResult GetFeeCountTrainer()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<double>() { isError = false, messages = "", data = dashboardService.GetFeeCounts(EProfileType.FitnessTrainner) });
        //}
        //public IActionResult GetFeeCountApp()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<double>() { isError = false, messages = "", data = dashboardService.GetFeeCounts(EProfileType.FitnessApp) });
        //}
        //public IActionResult GetFeeCountGym()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<double>() { isError = false, messages = "", data = dashboardService.GetFeeCounts(EProfileType.BusinessFitnessGym) });
        //}
        //public IActionResult GetFeeCountNeutrationApp()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<double>() { isError = false, messages = "", data = dashboardService.GetFeeCounts(EProfileType.BusinessFitnessNeutrationApp) });
        //}
        //public IActionResult GetFeeCountClass()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<double>() { isError = false, messages = "", data = dashboardService.GetFeeCounts(EProfileType.BusinessFitnessClass) });
        //}
        //public IActionResult GetFeeCountCustomer()
        //{
        //    return StatusCode(StatusCodes.Status200OK, new Response<double>() { isError = false, messages = "", data = dashboardService.GetFeeCounts(EProfileType.Customers) });
        //}
    }
}