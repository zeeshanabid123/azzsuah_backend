using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests.AdminRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.admin
{
   public interface IAdminGalleryService
    {
        List<AdminGalleryReponse> GetGallery(string SearchFilter, int skip, int take);
        AdminGalleryReponse GetGalleryById(Guid id);
        Task<bool> SaveAdminGallery(GalleryAdminRequest model );
        Task<bool> PostSatus(int flag, Guid id);

        Task<bool> EditGallery(GalleryAdminRequest model);
        Task<bool> DeleteGallery(Guid id);
        Task<bool> DeleteGalleryImage(Guid id);

    }
}
