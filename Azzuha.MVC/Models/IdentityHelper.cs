using Azzuha.Services.Interface.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azzuha.AdminPanel.Models
{
    public class IdentityHelper
    {
        private readonly IAdminAccountService adminAccountService;

        public IdentityHelper(IAdminAccountService adminAccountService)
        {
            this.adminAccountService = adminAccountService;
        }

        public string GetUserId(string userName)
        {
            return adminAccountService.FindUserByUsername(userName);
        }
    }
}
