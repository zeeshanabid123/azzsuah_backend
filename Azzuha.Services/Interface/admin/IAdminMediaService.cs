using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response;
using Azzuha.DataViewModels.Response.AdminResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.admin
{
   public interface IAdminMediaService
    {
        List<AdminMediaResponse> GetMedia(string SearchFilter, int skip, int take);
        AdminMediaResponse GetMediaById(Guid id);
        Task<bool> SaveAdminMedia(AdminMediaRequest model);
        Task<bool> PostSatus(int flag, Guid id);

        Task<bool> EditMedia(AdminMediaRequest model);
        Task<bool> DeleteMedia(Guid id);

         List<MediaTypeResponseModel> GetMediaTypes();
    }
}
