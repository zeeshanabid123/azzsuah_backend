using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response.AdminResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.admin
{
   public interface IUpEventAdminService
    {
        List<AdminEventResponse> GetEvents(string SearchFilter, int skip, int take);
        AdminEventResponse GetEventById(Guid id);
        Task<bool> SaveAdminEvents(EventAdminRequest model);
        Task<bool> PostSatus(int flag, Guid id);

        Task<bool> EditEvents(EventAdminRequest model);
        Task<bool> DeleteEvents(Guid id);
        List<AdminEventTypeResponse> GetEventsTypes();

    }
}
