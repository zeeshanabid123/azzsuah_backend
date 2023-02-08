using Azzuha.Data.Context;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.Services.Interface.admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Implementation.admin
{
    public  class AdminJsonEditorService : IAdminJsonEditorService
    {
        public async Task<bool> EditJsonData(AdminJsonEditorRequest request)
        {
            try
            {
                bool res = false;
                using (var db = new AzzuhaDbContext())
                {
                    try
                    {
                        var getaboutsUs = db.JsonData.Find(request.Id);
                        if (request != null)
                        {
                            getaboutsUs.Id = request.Id;
                            getaboutsUs.DataHeadingJson = request.DataHeadingJson;
                            getaboutsUs.UpdatedOn = DateTime.UtcNow;
                            getaboutsUs.IsEnabled = true;
                            getaboutsUs.UpdatedBy = "Admin";
                            db.Entry(getaboutsUs).State = EntityState.Modified;
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AdminJsonEditorRequest GetJsonDataById(Guid id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.JsonData.Where(x => x.Id == id).Select(x => new AdminJsonEditorRequest
                    {
                        Id = x.Id,
                        DataHeadingJson = x.DataHeadingJson,
                        IsEnabled = x.IsEnabled
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> saveJsonData(AdminJsonEditorRequest request, string userId)
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
                            await db.JsonData.AddAsync(new Data.DTOs.JsonData
                            {


                                Id = request.Id,
                               DataHeadingJson=request.DataHeadingJson,
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
