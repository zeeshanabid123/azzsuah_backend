using Azzuha.Common;
using Azzuha.Data.Context;
using Azzuha.Data.DTOs;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Enum;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Implementation.v1
{
    public class CommonService : ICommonService
    {
        private readonly IConfiguration configuration;
        public CommonService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<General<int>> GetGenders()
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Genders
                        .Where(x => x.IsEnabled)
                        .Select(x => new General<int>
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<General<int>> GetLanguages()
        //{
        //    try
        //    {
        //        using (var db = new AzzuhaDbContext())
        //        {
        //            return db.Languages
        //                .Where(x => x.IsEnabled)
        //                .Select(x => new General<int>
        //                {
        //                    Id = x.Id,
        //                    Name = x.Name
        //                }).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<General<int>> GetExperiences()
        //{
        //    try
        //    {
        //        using (var db = new AzzuhaDbContext())
        //        {
        //            return db.Expriences
        //                .Where(x => x.IsEnabled)
        //                .Select(x => new General<int>
        //                {
        //                    Id = x.Id,
        //                    Name = x.Name
        //                }).OrderBy(x => x.Name).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<General<int>> GetCountries()
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Countries
                        .Where(x => x.Flag)
                        .Select(x => new General<int>
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<General<int>> GetStates(int countryId)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.States
                        .Where(x => (x.Flag ?? false) && x.CountryId == countryId)
                        .Select(x => new General<int>
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<General<int>> GetCities(int stateId)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Cities
                        .Where(x => (x.Flag ?? false) && x.StateId == stateId)
                        .Select(x => new General<int>
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Tooltip = x.Latitude.ToString() + "," + x.Longitude.ToString(),
                        }).OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    

  

      




  


        public string GetContent(Guid contentType)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Contents.Where(x => x.IsEnabled && x.ContentTypeId == contentType).OrderByDescending(x => x.CreatedOn).FirstOrDefault()?.Text ?? "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEmailExists(string email)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    return db.Users.Any(x => x.Email.Equals(email));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> SaveContactQuery(QueryRequest request, Guid userId)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    await db.ContactQueries.AddAsync(new ContactQueries
                    {
                        Id = SystemGlobal.GetId(),
                        FirstName = request.FirstName,
                        LastName = request.FirstName,
                        Query = request.Query,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        IsEnabled = true,
                        CreatedBy = userId.ToString(),
                        CreatedOnDate = DateTime.UtcNow,
                        CreatedOn = DateTime.UtcNow,
                    });
                    await db.SaveChangesAsync();

                    _ = Task.Run(() =>
                     {
                         //Email.SendEmail("Contact Query From Fitcentr", request.Query, configuration.GetValue<string>("Email"), configuration.GetValue<string>("Password"), configuration.GetValue<string>("ContactEmail"), null, "");
                     });

                    return true;
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


        private IPInfoModel GetIpInfo(string ip)
        {
            IPInfoModel iPInfoModel = new IPInfoModel();
            try
            {
                string url = "https://ipinfo.io/" + ip + "?token=" + configuration.GetValue<string>("IPInfo:Token");
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                return JsonConvert.DeserializeObject<IPInfoModel>(response.Content);
            }
            catch (Exception)
            {

                return iPInfoModel;
            }
        }

        public JsonResponseModel GetJsonData(string id)
        {
            try
            {
                using (var db = new AzzuhaDbContext())
                {
                    var query = db.JsonData.Where(x=>x.Id== new Guid(id))
                         .Select(x => new JsonResponseModel
                         {
                             Id = x.Id,
                             DataHeadingJson = x.DataHeadingJson,
                             IsEnabled = x.IsEnabled,
                         })
                          .FirstOrDefault();

                    return query;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<MediaTypeResponseModel> GetMediaTypes()
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

        public List<AdminSliderResponse> GetSlider()
        {
            try
            {
                IQueryable<AdminSliderResponse> query;
                using (var db = new AzzuhaDbContext())
                {
                    query = db.MainSlider.Where(x=>x.IsEnabled==true)
                        .Select(x => new AdminSliderResponse
                        {
                            Id = x.Id,
                            Heading = x.Heading,
                            SubHeading = x.SubHeading,
                            ImageUrl=x.ImageUrl

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
