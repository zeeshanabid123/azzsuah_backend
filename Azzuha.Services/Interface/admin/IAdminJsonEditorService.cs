using Azzuha.DataViewModels.Requests.AdminRequests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.admin
{
   public interface IAdminJsonEditorService
    {
        Task<bool> saveJsonData(AdminJsonEditorRequest request, string userId);

        public AdminJsonEditorRequest GetJsonDataById(Guid id);
        public Task<bool> EditJsonData(AdminJsonEditorRequest request);
    }
}
