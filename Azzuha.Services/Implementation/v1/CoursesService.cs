using Azzuha.Common;
using Azzuha.Data.Context;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Implementation.v1
{
    public class CoursesService : ICoursesService
    {
        public List<CoursesResponseModel> GetCourses()
        {
            try
            {
                IQueryable<CoursesResponseModel> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.Courses.Where(x => x.IsEnabled == true)
                        .Select(x => new CoursesResponseModel
                        {
                            Id = x.Id,
                            CourseName = x.CourseName,
                            CourseImage = x.CourseImage,
                            CourseDuration = x.CourseDuration,
                            CourseDetail = x.CourseDetail,
                            CourseType = x.CourseType,
                            IsEnabled = x.IsEnabled,
                        })
                         .AsQueryable();

                    return query.ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SaveAmission(AdmissionRequest request)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    await db.CoursesAdmission.AddAsync(new Data.DTOs.CoursesAdmission
                    {
                        Id = SystemGlobal.GetId(),
                        Name = request.Name,
                        FatherName = request.FatherName,
                        City = request.City,
                        CurrentAddress = request.CurrentAddress,
                        Dob = request.Dob,
                        IdCardNumber = request.IdCardNumber,
                        EmailAddress = request.EmailAddress,
                        PrevoiusEducation = request.PrevoiusEducation,
                        PeducationFrom = request.PeducationFrom,
                        PhoneNumber = request.PhoneNumber,
                        SchoolRecordUrl=request.SchoolRecordUrl,
                        Country = request.Country,
                        BformUrl = request.BformUrl,
                        Cnicurl = request.Cnicurl,
                        CourseId = request.CourseId,
                        CourseTypeId = request.CourseTypeId,
                        IsEnabled = true,
                        CreatedBy = "User",
                        CreatedOnDate = DateTime.UtcNow,
                        CreatedOn = DateTime.UtcNow,
                    }); ; ;
                    await db.SaveChangesAsync();

                    _ = Task.Run(() =>
                    {
                        // Email.SendEmail("Contact Query From Fitcentr", request.Query, configuration.GetValue<string>("Email"), configuration.GetValue<string>("Password"), configuration.GetValue<string>("ContactEmail"), null, "");
                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
