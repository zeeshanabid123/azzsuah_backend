using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Azzuha.AdminPanel.Data;
using Azzuha.AdminPanel.Models;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Azzuha.AdminPanel.Controllers
{
    [Produces("application/json")]
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginViewModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IAdminAccountService _adminAccount;

        public AccountController(SignInManager<AppUser> signInManager,
            ILogger<LoginViewModel> logger,
            UserManager<AppUser> userManager, IEmailSender emailSender, IAdminAccountService adminAccount)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _adminAccount = adminAccount;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpGet]
        [Authorize]
        [Route("Identity/Account/Register")]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var signedUser = await _userManager.FindByEmailAsync(model.Email);
                if (signedUser!=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, model.Password, true, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        bool isBlock = _adminAccount.CheckUserStatus(signedUser.UserName, model.Password);
                        if (isBlock)
                        {
                            _logger.LogInformation("User logged in.");
                            return RedirectToLocal("/Dashboard/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You are Blocked.");
                            return View(model);
                        }

                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = true });

                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
              
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public async Task<IActionResult> RegisterUser(string stringId,string UserName, string FullName, string Designation, string Email, string PhoneNumber, string Password, string Role, string EnableFlag, int id, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (id == 1)
                {
                    var user = new AppUser
                {
                    UserName = UserName
                    ,
                    Email = Email,
                    PasswordHash = Password,
                    Designation = Designation,
                    PhoneNumber = PhoneNumber,
                    FullName = FullName,
                    UnhashPassword = Password,
                    EnableFlag = true,
                    EnalbleFalg = true,
                    CreationDate = DateTime.UtcNow,
                    UpdationDate = DateTime.UtcNow
                };


                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                        if (!string.IsNullOrEmpty(Role))
                        {
                            //ViewBag.roles = roleManager.Roles.ToList();

                            await _userManager.AddToRoleAsync(user, Role);

                        }
                        return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = result.Succeeded });
                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                }

                else if (id == 2)
                {
                    try
                    {
                        bool res = false;

                        if (stringId != null)
                        {
                            var user = await _userManager.FindByIdAsync(stringId.ToString());

                            user.UserName = UserName;
                            user.Email = Email;
                            user.Designation = Designation;
                            user.PhoneNumber = PhoneNumber;
                            user.FullName = FullName;
                            user.UnhashPassword = Password;
                            user.UpdationDate = DateTime.UtcNow;

                            if (user != null)
                            {
                                var result = await _userManager.UpdateAsync(user);

                                if (result.Succeeded)
                                {
                                    var aa = _userManager.GetRolesAsync(user);
                                    if (aa.Result.Count > 0)
                                    {
                                        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                                    }
                                    await _userManager.AddToRoleAsync(user, Role);

                                    await _userManager.RemovePasswordAsync(user);
                                    await _userManager.AddPasswordAsync(user, Password);
                                }

                                res = true;
                            }
                        }
                        return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = res });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return View(UserName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Identity/Account/Logout")]
        public IActionResult Logout()
        {
             _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> ChangeStatus(string id, int flg)
        {
            try
            {
                bool res = false;
                var user = await _userManager.FindByIdAsync(id);
                switch (flg)
                {
                    case 1:
                        if (user != null)
                        {
                            user.EnableFlag = user.EnableFlag != true;
                            var result = await _userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                res = true;
                            }
                        }

                        res = true;
                        break;
                    case 2:
                        if (user != null)
                        {
                            user.EnableFlag = user.EnableFlag == false;
                            var result = await _userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                res = true;
                            }
                        }
                        break;
                }

          

               
             
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetEditUser(string id)
        {
            try
            {
                var res = _adminAccount.GetUser(id);
                var user = await _userManager.FindByIdAsync(id);
                string[] temp = res.PhoneNumber.Split('-');
                res.PhoneNumber = temp[1];
                res.DailCode = temp[0];

                res.Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

                var drp = _adminAccount.GetPhoneNumberByDialCode(res.DailCode);
                return StatusCode(StatusCodes.Status200OK, new Response<object>() { isError = false, messages = "", data = new { res = res, drp = drp } });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IActionResult> GetUserList(string SearchFilter, int skip, int take)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<UserList>>() { isError = false, messages = "", data = _adminAccount.GetUsers(SearchFilter, skip, take) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> GetRoles()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, new Response<List<RoleList>>() { isError = false, messages = "", data = _adminAccount.getRoleList() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> UserName(string name)
        {
            try
            {
                bool res = false;
                var userId = _adminAccount.FindUserByUsername(name);

                if (string.IsNullOrEmpty(userId))
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> EmailChk(string email)
        {
            try
            {
                bool res = false;
                var userId = _adminAccount.FindUserByEmail(email);

                if (string.IsNullOrEmpty(userId))
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
                return StatusCode(StatusCodes.Status200OK, new Response<bool>() { isError = false, messages = "", data = res });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Identity/Account/Login")]
        public IActionResult LoginRedirect(string ReturnUrl)
        {
            return RedirectToAction("Login", "Account");
        }

       
    }

}