using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Azzuha.AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Azzuha.Services.Interface.admin;

namespace Azzuha.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminAccountService adminAccount;
        public HomeController(ILogger<HomeController> logger , IAdminAccountService adminAccount)
        {
            _logger = logger;
            this.adminAccount = adminAccount;
        }
[AllowAnonymous]
    public IActionResult Index()
{
    return View();
}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
