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
    public class UpEventAdminService : IUpEventAdminService
    {
        public async Task<bool> DeleteEvents(Guid id)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getevent = db.UpComingEvents.Find(id);
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

        public async Task<bool> EditEvents(EventAdminRequest model)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getevent = db.UpComingEvents.Find(model.Id);
                    if (model != null)
                    {
                        getevent.Id = model.Id;
                        getevent.ImageUrl = model.ImageUrl;
                        getevent.Name = model.Name;
                        getevent.EventTypeId = model.EventTypeId;
                        getevent.IsEnabled = model.IsEnabled;

                        getevent.UpdatedBy = "Admin";
                        getevent.UpdatedOn = DateTime.UtcNow;
                        db.Entry(getevent).State = EntityState.Modified;
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

        public AdminEventResponse GetEventById(Guid id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.UpComingEvents
                        .Where(x => x.Id.Equals(id))
                        .Select(x => new AdminEventResponse
                        {
                            

                            Id = x.Id,
                            Name = x.Name,
                            EventTypeId=x.EventTypeId,
                            ImageUrl=x.ImageUrl,
                            IsEnabled = x.IsEnabled,

                        })
                        .FirstOrDefault() ?? new AdminEventResponse();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AdminEventResponse> GetEvents(string SearchFilter, int skip, int take)
        {
            try
            {
                IQueryable<AdminEventResponse> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.UpComingEvents
                        .Select(x => new AdminEventResponse
                        {
                            Id = x.Id,
                            Name = x.Name,
                            EventTypeId = x.EventTypeId,
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
                            x => x.Name.ToLower().Contains(SearchFilter.ToLower())
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

        public List<AdminEventTypeResponse> GetEventsTypes()
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                   var query = db.EventTypes
                        .Select(x => new AdminEventTypeResponse
                        {
                            Id = x.Id,
                            EventName = x.EventName,
                            IsEnabled = x.IsEnabled,
                        })
                         .ToList();

                    return query;
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
                    var model = db.UpComingEvents.Find(id);
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

        public async Task<bool> SaveAdminEvents(EventAdminRequest model)
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
                            await db.UpComingEvents.AddAsync(new Data.DTOs.UpComingEvents
                            {
                                Id = model.Id,
                                Name = model.Name,
                                ImageUrl = model.ImageUrl,
                                IsEnabled = true,
                                EventTypeId=model.EventTypeId,
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
