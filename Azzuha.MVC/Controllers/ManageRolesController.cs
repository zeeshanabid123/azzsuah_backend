using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azzuha.AdminPanel.Data;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Azzuha.AdminPanel.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class ManageRolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAdminAccountService _adminAccount;
        private RoleManager<IdentityRole> _roleManager;
        public ManageRolesController(SignInManager<AppUser> signInManager,
         RoleManager<IdentityRole> roleMgr,
          UserManager<AppUser> userManager,  IAdminAccountService adminAccount)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _adminAccount = adminAccount;
            _roleManager = roleMgr;
        }

        public async Task<IActionResult> Roles()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<IdentityRole>>() { isError = false, messages = "", data = _roleManager.Roles.ToList() });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> RolesView()
        {
            try
            {
                var roles = _roleManager.Roles.OrderBy(x => x.Name).ToList();
                roles.Remove(roles.Where(x => x.Name.Equals("SuperAdmin")).FirstOrDefault());
                roles.Remove(roles.Where(x => x.Id.Equals("210afb2f-62e8-4806-9572-b9274bcabd97")).FirstOrDefault());
                return StatusCode(StatusCodes.Status200OK, new Response<List<IdentityRole>>() { isError = false, messages = "", data = roles });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<IActionResult> SaveRole(string Name, string id)
        {
            try
            { bool res = false;

                var chk = _roleManager.FindByIdAsync(id).Result;
                if (chk == null)
                {
                    if (!await _roleManager.RoleExistsAsync(Name))
                    {
                        var role = new IdentityRole();
                        role.Name = Name;
                        await _roleManager.CreateAsync(role);
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                }
                else
                {
                    var role = _roleManager.FindByIdAsync(id);
                    role.Result.Name = Name;
                    await _roleManager.UpdateAsync(role.Result);

                    res = true;
                }

                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> EditRole(string nameParam)
        {
            try
            {
                bool res = true;
               

                var role = _roleManager.FindByNameAsync(nameParam);
                return StatusCode(StatusCodes.Status200OK, new Response<object>() { isError = false, messages = "", data = new { Name = role.Result.Name, Id = role.Result.Id } });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> deleteRole(string nameParam)
        {
            try
            {
                bool res = false;
                int count = _adminAccount.UserCountByRoleName(nameParam);
                if (count == 0)
                {
                

                    if (await _roleManager.RoleExistsAsync(nameParam))
                    {
                        var role = _roleManager.FindByNameAsync(nameParam);
                        await _roleManager.DeleteAsync(role.Result);
                    }
                    res = true;
                }
                else if (count > 0)
                {
                    res = false;
                }
                return StatusCode(StatusCodes.Status200OK, new Response<object>() { isError = false, messages = "", data = res});
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IActionResult> GetCountryCode()
        {
            try
            {
                
                return StatusCode(StatusCodes.Status200OK, new Response<List<CountryCodes>>() { isError = false, messages = "", data = _adminAccount.GetCountryCode()});
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
        public IActionResult Index()
        {
            return View();
        }
    }
}
