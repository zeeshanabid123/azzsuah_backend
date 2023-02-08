using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Azzuha.AdminPanel.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class RightsManagerController : Controller
    {
        private readonly IAdminAccountService _adminAccount;

        public RightsManagerController(  IAdminAccountService adminAccount)
        {
            _adminAccount = adminAccount;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetRigts(string role)
        {
            try
            {
                RightsViewModel ress = new RightsViewModel { DashBorad = false, UserManager = false };

                var rights = _adminAccount.GetRightByRoleId(role);
                if (rights.Count > 0)
                {
                    foreach (var name in rights)
                    {
                        switch (name)
                        {
                            case "Dashboard":
                                ress.DashBorad = true;
                                break;
                            case "User Management":
                                ress.UserManager = true;
                                break;
                            case "AdminBlog":
                                ress.AdminBlog = true;
                                break;
                            case "MarketingLibaray":
                                ress.MarketingLibaray = true;
                                break;
                            case "Voucher":
                                ress.Voucher = true;
                                break;
                            case "Report":
                                ress.Report = true;
                                break;
                            case "Profile":
                                ress.Profile = true;
                                break;

                            default:
                                break;
                        }
                    }
                }
                    return StatusCode(StatusCodes.Status200OK, new Response<RightsViewModel>() { isError = false, messages = "", data = ress });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> SaveRight(string saveroleid, int saverightid)
        {
            try
            {
                bool res = _adminAccount.SaveRight(saveroleid, saverightid);
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> RemoveRight(string roleid, int rightid)
        {
            try
            {
                bool res = _adminAccount.RemoveRight(roleid, rightid);
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
