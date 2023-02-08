using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.v1
{
   public interface ICoursesService
    {
        Task<bool> SaveAmission(AdmissionRequest request);
        public List<CoursesResponseModel> GetCourses();
    }
}
