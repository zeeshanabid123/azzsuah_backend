using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response.AdminResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.admin
{
  public  interface IAdminSliderService
    {
        List<AdminSliderResponse> GetSlider(string SearchFilter, int skip, int take);
        AdminSliderResponse GetSliderById(Guid id);
        Task<bool> SaveAdminSlider(AdminSliderRequest model);
        Task<bool> PostSatus(int flag, Guid id);

        Task<bool> EditSlider(AdminSliderRequest model);
        Task<bool> DeleteSlider(Guid id);
    }
}
