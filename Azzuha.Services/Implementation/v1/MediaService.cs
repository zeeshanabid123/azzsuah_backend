using Azzuha.Data.Context;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azzuha.Services.Implementation.v1
{
    public class MediaService : IMediaService
    {
        public List<MediaResponseModel> GetMediaById(Guid? MediaTypeId)
        {
            try
            {
                IQueryable<MediaResponseModel> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.Media.Where(x=>x.MediaTypeId==MediaTypeId && x.IsEnabled==true)
                        .Select(x => new MediaResponseModel
                        {
                            
                          Id=x.Id,
                          MediaTypeId=x.MediaTypeId,
                          FileUrl=x.FileUrl,
                          IsAudio=x.IsAudio,
                          Name=x.Name
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
