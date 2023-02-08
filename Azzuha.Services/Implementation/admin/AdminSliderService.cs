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
    public class AdminSliderService : IAdminSliderService
    {
        public async Task<bool> DeleteSlider(Guid id)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getevent = db.MainSlider.Find(id);
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

        public async Task<bool> EditSlider(AdminSliderRequest model)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getcourse = db.MainSlider.Find(model.Id);
                    if (getcourse != null)
                    {
                        getcourse.Id = model.Id;
                        getcourse.SubHeading = model.SubHeading;
                        getcourse.Heading = model.Heading;
                        getcourse.ImageUrl = model.ImageUrl;
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

        public List<AdminSliderResponse> GetSlider(string SearchFilter, int skip, int take)
        {
            try
            {
                IQueryable<AdminSliderResponse> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.MainSlider
                        .Select(x => new AdminSliderResponse
                        {
                            Id = x.Id,
                            SubHeading = x.SubHeading,
                            Heading = x.Heading,
                            ImageUrl = x.ImageUrl,
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
                            x => x.Heading.ToLower().Contains(SearchFilter.ToLower())
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

        public AdminSliderResponse GetSliderById(Guid id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.MainSlider
                        .Where(x => x.Id.Equals(id))
                        .Select(x => new AdminSliderResponse
                        {


                            Id = x.Id,
                            Heading = x.Heading,
                            ImageUrl = x.ImageUrl,
                            SubHeading = x.SubHeading,
                            IsEnabled = x.IsEnabled,



                        })
                        .FirstOrDefault() ?? new AdminSliderResponse();
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
                    var model = db.MainSlider.Find(id);
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

        public async Task<bool> SaveAdminSlider(AdminSliderRequest model)
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
                            await db.MainSlider.AddAsync(new Data.DTOs.MainSlider
                            {


                                Id = model.Id,
                                Heading = model.Heading,
                                SubHeading = model.SubHeading,
                                ImageUrl = model.ImageUrl,
                                IsEnabled = model.IsEnabled,
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
