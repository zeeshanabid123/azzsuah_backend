using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response.AdminResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Interface.admin
{
   public interface IAdminCoursesService
    {
        List<AdminCourseResponse> GetCourses(string SearchFilter, int skip, int take);
        AdminCourseResponse GetCourseById(Guid id);
        Task<bool> SaveAdminCourse(AdminCourseRequest model);
        Task<bool> PostSatus(int flag, Guid id);

        Task<bool> EditCourse(AdminCourseRequest model);
        Task<bool> DeleteCourse(Guid id);
    }
}
