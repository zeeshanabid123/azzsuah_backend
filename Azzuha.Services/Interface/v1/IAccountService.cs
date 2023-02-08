using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.v1
{
    public interface IAccountService
    {
        Tuple<bool, LoginResponse> Login(LoginRequest login);
        Task<bool> ForgotSentVerificationCode(string email);
        Task<bool> ChangePassword(ChangePassword request);
    }
}
