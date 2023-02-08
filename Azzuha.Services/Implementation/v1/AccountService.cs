using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Azzuha.Common;
using Azzuha.Data.Context;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Enum;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Implementation.v1
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration configuration;

        public AccountService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Tuple<bool, LoginResponse> Login(LoginRequest login)
        {
            try
            {
                bool isLogin = false;
                LoginResponse response = null;
                var encryptedPassword = new Encryption().Encrypt(login.Password, configuration.GetValue<string>("EncryptionKey"));
                using (var db = new AzzuhaDbContext())
                {

                    var user = db.Users.FirstOrDefault(x => x.IsEnabled && x.Email.Equals(login.Email) && x.Password.Equals(encryptedPassword));

                    if (user == null) return Tuple.Create(false, response);

                    var authToken = new Encryption().GetToken(new AuthToken { UserId = user.Id, ProfileTypeId = user.ProfileTypeId.GetValueOrDefault() }, configuration.GetValue<string>("Tokens:Key"));

                    string name = "", imageThumbnailUrl = "", profileType = user.ProfileTypeId.ToString().ToUpper();



                    response = new LoginResponse { Token = authToken, UserId = user.Id, ProfileTypeId = user.ProfileTypeId.GetValueOrDefault(), Email = user.Email, Name = name, ImageThumbnailUrl = imageThumbnailUrl, IsSubscribed = user.IsSubscribed.GetValueOrDefault() };
                    isLogin = true;
                }

                return Tuple.Create(isLogin, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Forgot Password
        public async Task<bool> ForgotSentVerificationCode(string email)
        {
            try
            {
                bool response = false;

                using (var db = new AzzuhaDbContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));

                    if (user != null)
                    {
                        user.ForgotLinkKey = SystemGlobal.GetId();
                        user.ForgotExpiry = DateTime.UtcNow.AddHours(1);

                        db.Entry(user).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        _ = Task.Run(() =>
                        {
                            _ = Email.SendEmail("fitcentr Forgot password link", "<a href = 'https://fitcentrapp.stagingdesk.com/account/reset/" + user.ForgotLinkKey + "'>press this link to reset your password</a>", configuration.GetValue<string>("Email"), configuration.GetValue<string>("Password"), email, null, "");
                        });

                        response = true;
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ChangePassword(ChangePassword request)
        {
            try
            {
                bool response = false;

                using (var db = new AzzuhaDbContext())
                {
                    var user = db.Users.FirstOrDefault(x => x.ForgotLinkKey.Equals(request.ForgotLinkKey) && x.ForgotExpiry >= DateTime.UtcNow);

                    if (user != null)
                    {
                        user.Password = new Encryption().Encrypt(request.NewPassword, configuration.GetValue<string>("EncryptionKey"));
                        user.UpdatedBy = user.Id.ToString();
                        user.UpdatedOn = DateTime.UtcNow;

                        db.Entry(user).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        response = true;
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
