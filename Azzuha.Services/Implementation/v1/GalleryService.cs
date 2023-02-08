using Azzuha.Data.Context;
using Azzuha.DataViewModels.Requests.AdminRequests;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azzuha.Services.Implementation.v1
{
    public class GalleryService : IGalleryService
    {
        public List<GalleryResponseModel> GetGallery()
        {
            try
            {
                IQueryable<GalleryResponseModel> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.Gallery.Where(x=>x.IsEnabled==true)
                        .Select(x => new GalleryResponseModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            IsEnabled = x.IsEnabled,
                            GalleryImagesmodel=x.GalleryImages.Where(x=>x.Id==x.Id && x.IsEnabled==true).Select(x=> new AdminGalleryImageRequest() { 
                            GalleryImageUrl=x.GalleryImageUrl,
                            Id=x.Id
                            }).ToList()
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
