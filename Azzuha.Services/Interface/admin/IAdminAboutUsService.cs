using Azzuha.DataViewModels.Requests.AdminRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.admin
{
  public  interface IAdminAboutUsService
    {
         Task<bool> saveAbouts(AboutUsAdminRequest request, string userId);
        public AboutUsAdminRequest GetAboutUsById(Guid id);
        public  Task<bool> EditAboutUs(AboutUsAdminRequest request);

    }
}
