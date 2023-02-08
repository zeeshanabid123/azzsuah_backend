
using Azzuha.Data.DTOs;
using Azzuha.Services.Interface.admin;
using Azzuha.Services.Interface.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Azzuha.AdminPanel.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class VerifyRights : Attribute, IAuthorizationFilter
    {
        public int actionRight { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var accountService = (IAdminAccountService)context.HttpContext.RequestServices.GetService(typeof(IAdminAccountService));
            var userName = Convert.ToString(context.HttpContext.User.Identity.Name);

            try
            {
                string r_id = accountService.GetRoleIdByUsername(userName);

                if (!r_id.Equals("8bd498d1-9b81-48bb-8c99-b6c349e325aa"))
                {
                    var UserRight = accountService.IsRightExist(r_id, actionRight);
                    if (!UserRight)
                    {
                        //FormsAuthentication.SignOut();
                        //filterContext.RequestContext.HttpContext.Server.TransferRequest("~/Account/LogOff1/?NOT_AUTHORISED=TRUE", false);
                    }
                }
            }
            catch (Exception ex)
            {
                //FormsAuthentication.SignOut();
                //filterContext.RequestContext.HttpContext.Server.TransferRequest("~/Account/LogOff1/?NOT_AUTHORISED=TRUE", false);
            }
        }
    }
}