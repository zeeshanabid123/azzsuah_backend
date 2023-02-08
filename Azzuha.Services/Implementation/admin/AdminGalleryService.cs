using Azzuha.Common;
using Azzuha.Data.Context;
using Azzuha.DataViewModels.Common;
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
    public class AdminGalleryService : IAdminGalleryService
    {
        public async Task<bool> DeleteGallery(Guid id)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getevent = db.Gallery.Find(id);
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

        public async Task<bool> DeleteGalleryImage(Guid id)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getevent = db.GalleryImages.Find(id);
                    if (getevent != null)
                    {
                        getevent.IsEnabled = false;
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

        public async Task<bool> EditGallery(GalleryAdminRequest model)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getgallery = db.Gallery.Find(model.Id);
                    if (model != null)
                    {
                        getgallery.Id = model.Id;
                        getgallery.Name = model.Name;
                        getgallery.IsEnabled = model.IsEnabled;

                        getgallery.UpdatedBy = "Admin";
                        getgallery.UpdatedOn = DateTime.UtcNow;
                        db.Entry(getgallery).State = EntityState.Modified;
                        db.SaveChanges();
                        if (model.GalleryImagesmodel.Count > 0)
                        {
                            foreach (var i in model.GalleryImagesmodel)
                            {
                                await db.GalleryImages.AddAsync(new Data.DTOs.GalleryImages
                                {
                                    Id = SystemGlobal.GetId(),
                                    GalleryId = model.Id,
                                    GalleryImageUrl = i.GalleryImageUrl,
                                    IsEnabled = model.IsEnabled,
                                    CreatedOn = DateTime.UtcNow,
                                    CreatedOnDate = DateTime.UtcNow.Date,
                                    CreatedBy = "Admin",

                                });
                            }

                            await db.SaveChangesAsync();
                        }
                      
                       

                    }

                  
                    res = true;


                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return res;
        }

        public List<AdminGalleryReponse> GetGallery(string SearchFilter, int skip, int take)
        {
            try
            {
                IQueryable<AdminGalleryReponse> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.Gallery
                        .Select(x => new AdminGalleryReponse
                        {
                            Id = x.Id,
                            Name = x.Name,
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

        public AdminGalleryReponse GetGalleryById(Guid id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Gallery
                        .Where(x => x.Id.Equals(id))
                        .Select(x => new AdminGalleryReponse
                        {


                            Id = x.Id,
                            Name = x.Name,
                          
                            IsEnabled = x.IsEnabled,
                            GalleryImagesmodel=x.GalleryImages.Where(x=>x.GalleryId.Value==id && x.IsEnabled==true).Select(x=> new AdminGalleryImageRequest { 
                            Id=x.Id,
                            GalleryImageUrl=x.GalleryImageUrl
                            }).ToList()


                        })
                        .FirstOrDefault() ?? new AdminGalleryReponse();
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
                    var model = db.Gallery.Find(id);
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

        public async Task<bool> SaveAdminGallery(GalleryAdminRequest model)
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
                          var gallery=  await db.Gallery.AddAsync(new Data.DTOs.Gallery
                            {
                                Id = SystemGlobal.GetId(),
                                Name = model.Name,
                                IsEnabled = model.IsEnabled,
                                CreatedOn = DateTime.UtcNow,
                                CreatedOnDate = DateTime.UtcNow.Date,
                                CreatedBy = "Admin",
                            });
                            await db.SaveChangesAsync();
                            foreach (var i in model.GalleryImagesmodel)
                            {
                                await db.GalleryImages.AddAsync(new Data.DTOs.GalleryImages
                                {
                                    Id= SystemGlobal.GetId(),
                                    GalleryId=gallery.Entity.Id,
                                    GalleryImageUrl=i.GalleryImageUrl,
                                    IsEnabled = model.IsEnabled,
                                    CreatedOn = DateTime.UtcNow,
                                    CreatedOnDate = DateTime.UtcNow.Date,
                                    CreatedBy = "Admin",

                                });
                            }
                           
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
