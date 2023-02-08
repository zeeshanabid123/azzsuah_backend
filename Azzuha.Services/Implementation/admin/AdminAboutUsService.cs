using Azzuha.Common;
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
    public class AdminAboutUsService : IAdminAboutUsService
    {
        public async Task<bool> EditAboutUs(AboutUsAdminRequest request)
        {
            try
            {
                bool res = false;
                using (var db = new AzzuhaDbContext())
                {
                    try
                    {
                        var getaboutsUs = db.AboutUs.Find(request.Id);
                        if (request != null)
                        {
                            getaboutsUs.Id = request.Id;
                            getaboutsUs.Text = request.Text;
                            getaboutsUs.UpdatedOn = DateTime.UtcNow;
                            getaboutsUs.IsEnabled = request.IsEnabled;
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

        public AboutUsAdminRequest GetAboutUsById(Guid id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.AboutUs.Where(x=>x.Id==id).Select(x=> new AboutUsAdminRequest {
                    Id=x.Id,
                    Text=x.Text,
                    IsEnabled=x.IsEnabled
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      

        public async Task<bool> saveAbouts(AboutUsAdminRequest request, string  userId)
        {
            using (var db = new AzzuhaDbContext())
            {
                await db.AboutUs.AddAsync(new Data.DTOs.AboutUs
                {
                    Id = SystemGlobal.GetId(),
                   Text=request.Text,
                    IsEnabled = true,
                    CreatedOn = DateTime.UtcNow,
                    CreatedOnDate = DateTime.UtcNow,
                    CreatedBy = userId.ToString(),
                });
                await db.SaveChangesAsync();

                return true;
            }
        }
    }
}
