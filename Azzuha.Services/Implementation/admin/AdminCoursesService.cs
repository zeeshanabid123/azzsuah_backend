using Azzuha.Data.Context;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Implementation.admin
{
    public class AdminCoursesService : IAdminCoursesService
    {
        public async Task<bool> DeleteCourse(Guid id)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getevent = db.Courses.Find(id);
                    if (getevent != null)
                    {
                        db.Entry(getevent).State = EntityState.Deleted;
                    }
                    db.SaveChanges();
                    res = true;


                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return res;
        }

        public async Task<bool> EditCourse(AdminCourseRequest model)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getcourse = db.Courses.Find(model.Id);
                    if (getcourse != null)
                    {
                        getcourse.Id = model.Id;
                        getcourse.CourseName = model.CourseName;
                        getcourse.CourseImage = model.CourseImage;
                        getcourse.CourseDuration = model.CourseDuration;
                        getcourse.CourseDetail = model.CourseDetail;
                        getcourse.CourseType = model.CourseType;

                        getcourse.IsEnabled = model.IsEnabled;

                        getcourse.UpdatedBy = "Admin";
                        getcourse.UpdatedOn = DateTime.UtcNow;
                        db.Entry(getcourse).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                    res = true;


                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return res;
        }

        public AdminCourseResponse GetCourseById(Guid id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Courses
                        .Where(x => x.Id.Equals(id))
                        .Select(x => new AdminCourseResponse
                        {


                            Id = x.Id,
                            CourseName = x.CourseName,
                            CourseImage = x.CourseImage,
                            CourseDuration = x.CourseDuration,
                            CourseDetail = x.CourseDetail,
                            CourseType = x.CourseType,
                            IsEnabled = x.IsEnabled,
                            


                        })
                        .FirstOrDefault() ?? new AdminCourseResponse();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AdminCourseResponse> GetCourses(string SearchFilter, int skip, int take)
        {
            try
            {
                IQueryable<AdminCourseResponse> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.Courses
                        .Select(x => new AdminCourseResponse
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

                    if (!string.IsNullOrEmpty(SearchFilter))
                    {
                        int totalCases = -1;
                        var isNumber = Int32.TryParse(SearchFilter, out totalCases);
                        if (isNumber)
                        {
                            //query = query.Where(
                            //    x => x.TotalCases == totalCases
                            //);
                        }
                        else
                        {
                            query = query.Where(
                            x => x.CourseName.ToLower().Contains(SearchFilter.ToLower())
                        );
                        }
                    }
                    return query.Skip(skip).Take(take).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> PostSatus(int flag, Guid id)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var model = db.Courses.Find(id);
                    switch (flag)
                    {
                        case 1:
                            if (model != null)
                            {
                                model.IsEnabled = model.IsEnabled != true;
                                db.Entry(model).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                            res = true;
                            break;
                        case 2:
                            if (model != null)
                            {
                                model.IsEnabled = model.IsEnabled == false;
                                db.Entry(model).State = EntityState.Modified;
                            }

                            db.SaveChanges();
                            res = true;
                            break;
                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return res;
        }
        public async Task<bool> SaveAdminCourse(AdminCourseRequest model)
        {
            try
            {
                bool response = false;
                using (var db = new AzzuhaDbContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //Save user
                            await db.Courses.AddAsync(new Data.DTOs.Courses
                            {


                                Id = model.Id,
                                CourseName = model.CourseName,
                                CourseImage = model.CourseImage,
                                CourseDuration = model.CourseDuration,
                                CourseDetail = model.CourseDetail,
                                CourseType = model.CourseType,
                                IsEnabled = true,
                                CreatedOn = DateTime.UtcNow,
                                CreatedOnDate = DateTime.UtcNow.Date,
                                CreatedBy = "Admin",
                            });
                            await db.SaveChangesAsync();
                            transaction.Commit();

                            response = true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

      
