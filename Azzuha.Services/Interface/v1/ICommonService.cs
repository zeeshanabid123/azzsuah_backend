using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.DataViewModels.Response.AdminResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.v1
{
    public interface ICommonService
    {
        List<General<int>> GetGenders();
        List<General<int>> GetCountries();
        List<General<int>> GetStates(int countryId);
        List<General<int>> GetCities(int stateId);
        string GetContent(Guid contentType);
        bool IsEmailExists(string email);
        Task<bool> SaveContactQuery(QueryRequest request, Guid userId);
        public List<AdminEventTypeResponse> GetEventsTypes();

        public JsonResponseModel GetJsonData(string id);

        public List<MediaTypeResponseModel> GetMediaTypes();
        public List<AdminSliderResponse> GetSlider();

  

    }
}
