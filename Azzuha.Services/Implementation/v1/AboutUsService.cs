using Azzuha.Data.Context;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azzuha.Services.Implementation.v1
{
  public  class AboutUsService: IAboutUsService
    {
        public AboutUsResponseModel GetAboutUsById(string id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.AboutUs.Where(x => x.Id == new Guid(id)).Select(x => new AboutUsResponseModel
                    {
                        Id = x.Id,
                        Text = x.Text,
                        IsEnabled = x.IsEnabled
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
