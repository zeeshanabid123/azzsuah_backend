using Azzuha.Data.Context;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response;
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
    public class AdminMediaService : IAdminMediaService
    {
        public async Task<bool> DeleteMedia(Guid id)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getevent = db.Media.Find(id);
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

        public async Task<bool> EditMedia(AdminMediaRequest model)
        {
            bool res = false;
            using (var db = new AzzuhaDbContext())
            {
                try
                {
                    var getMedia = db.Media.Find(model.Id);
                    if (getMedia != null)
                    {
                        getMedia.Id = model.Id;
                        getMedia.Name = model.Name;
                        getMedia.IsAudio = model.IsAudio;
                        getMedia.FileUrl = model.FileUrl;
                        getMedia.MediaTypeId = model.MediaTypeId;
                        getMedia.IsEnabled = model.IsEnabled;

                        getMedia.UpdatedBy = "Admin";
                        getMedia.UpdatedOn = DateTime.UtcNow;
                        db.Entry(getMedia).State = EntityState.Modified;
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

        public List<AdminMediaResponse> GetMedia(string SearchFilter, int skip, int take)
        {
            try
            {
                IQueryable<AdminMediaResponse> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.Media
                        .Select(x => new AdminMediaResponse
                        {

                            Id = x.Id,
                            Name = x.Name,
                            IsAudio = x.IsAudio,
                            MediaTypeId = x.MediaTypeId,
                            FileUrl = x.FileUrl,

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

        public AdminMediaResponse GetMediaById(Guid id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Media
                        .Where(x => x.Id.Equals(id))
                        .Select(x => new AdminMediaResponse
                        {


                            Id = x.Id,
                            Name = x.Name,
                            IsAudio=x.IsAudio,
                            MediaTypeId=x.MediaTypeId,
                            FileUrl=x.FileUrl,

                            IsEnabled = x.IsEnabled,



                        })
                        .FirstOrDefault() ?? new AdminMediaResponse();
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
                    var model = db.Media.Find(id);
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

        public async Task<bool> SaveAdminMedia(AdminMediaRequest model)
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
                            await db.Media.AddAsync(new Data.DTOs.Media
                            {


                                Id = model.Id,
                                Name=model.Name,
                                FileUrl=model.FileUrl,
                                MediaTypeId=model.MediaTypeId,
                                IsAudio=model.IsAudio,
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


        public  List<MediaTypeResponseModel> GetMediaTypes()
        {
            try
            {
                IQueryable<MediaTypeResponseModel> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.MediaTypes
                        .Select(x => new MediaTypeResponseModel
                        {
                            Id = x.Id,
                            MediaTypeImageUrl = x.MediaImage,
                            MediaTypeName = x.MediaTypeName

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
    }
}
