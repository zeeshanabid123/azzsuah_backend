using Azzuha.Common;
using Azzuha.DataViewModels.Common;
using Azzuha.Services.Interface.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Apis.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private bool isNecessary = false;
        public CustomAuthorizeAttribute(bool isNecessary)
        {
            this.isNecessary = isNecessary;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            AuthToken tokenData = null;
            string token = string.Empty;
            token = (context.HttpContext.Request.Headers.Any(x => x.Key == "Authorization")) ? context.HttpContext.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value.SingleOrDefault().Replace("Bearer ", "") : "";

            if (token == string.Empty && isNecessary)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(new Response<bool> { isError = true, messages = Error.MissingAuthorization, data = false });
                return;
            }

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));
                    var keyByteArray = Encoding.ASCII.GetBytes(configuration.GetValue<String>("Tokens:Key"));
                    var signinKey = new SymmetricSecurityKey(keyByteArray);

                    SecurityToken validatedToken;
                    var handeler = new JwtSecurityTokenHandler();
                    var we = handeler.ValidateToken(token, new TokenValidationParameters
                    {
                        IssuerSigningKey = signinKey,
                        ValidAudience = "Audience",
                        ValidIssuer = "Issuer",
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(0)
                    }, out validatedToken);

                    var temp = handeler.ReadJwtToken(token);
                    tokenData = JsonConvert.DeserializeObject<AuthToken>(temp.Claims.FirstOrDefault(x => x.Type.Equals("token"))?.Value);
                    context.RouteData.Values.Add("userId", tokenData.UserId);
                    context.RouteData.Values.Add("profileType", tokenData.ProfileTypeId);
                }
                catch (Exception ex)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult(new Response<bool> { isError = true, messages = Error.AccessDenied, data = false });
                    return;
                }
            }
            else
            {
                context.RouteData.Values.Add("userId", Guid.Empty);
                context.RouteData.Values.Add("profileType", Guid.Empty);
            }
        }
    }
}
