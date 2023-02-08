using Azzuha.Data.Context;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Enum;
using Azzuha.DataViewModels.Response.AdminResponse;
using Azzuha.Services.Interface.admin;
using Azzuha.Services.Interface.v1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azzuha.Services.Implementation.admin
{
    public class AdminListingService : IAdminListingService
    {
        public GenralReportListRequest<AdminResponseViewModel> GetAdmintionReports(GenralReportListRequest<AdminResponseViewModel> page, DateTime? str, DateTime? endd)
        {
            DateTime strt = Convert.ToDateTime(str);
            DateTime end = Convert.ToDateTime(endd).AddDays(1);
            GenralReportListRequest<AdminResponseViewModel> data = new GenralReportListRequest<AdminResponseViewModel>();
            DateTime datte = DateTime.UtcNow;
            using (var db = new AzzuhaDbContext())
            {
                var query = db
                    .CoursesAdmission.Where(x => x.CreatedOn >= strt && x.CreatedOn <= end)
               .Select(x => new AdminResponseViewModel
               {
                   
                   BformUrl=x.BformUrl,
                   City=x.City,
                   Cnicurl=x.Cnicurl,
                   Country=x.Country,
                   Dob=x.Dob,
                   CurrentAddress=x.CurrentAddress,
                   FatherName=x.FatherName,
                   EmailAddress=x.EmailAddress,
                   IdCardNumber=x.IdCardNumber,
                   Name=x.Name,
                   PrevoiusEducation=x.PrevoiusEducation,
                   PeducationFrom=x.PeducationFrom,
                 SchoolRecordUrl=x.SchoolRecordUrl,
                 
                   PhoneNumber = x.PhoneNumber,
                   CreatedOnDate = x.CreatedOnDate,
               })
               .OrderByDescending(x => x.CreatedOnDate)
               .AsQueryable();

                if (!string.IsNullOrEmpty(page.Search))
                {
                    var date = new DateTime();
                    var sdate = DateTime.TryParse(page.Search, out date);
                    int number = -1;
                    var isNumber = Int32.TryParse(page.Search, out number);

                }
                var orderedQuery = query.OrderByDescending(x => x.CreatedOnDate);
                switch (page.SortIndex)
                {
                    case 0:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.FatherName) : query.OrderBy(x => x.FatherName);
                        break;
                    case 1:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                        break;
                    case 2:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.EmailAddress) : query.OrderBy(x => x.EmailAddress);
                        break;
                    case 3:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.PhoneNumber) : query.OrderBy(x => x.PhoneNumber);
                        break;
                    case 4:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.CreatedOnDate) : query.OrderBy(x => x.CreatedOnDate);
                        break;

                }


                data.Page = page.Page;
                data.PageSize = page.PageSize;
                data.TotalRecords = orderedQuery.Count();
                data.Draw = page.Draw;
                data.Entities = orderedQuery.Skip(page.Page).Take(page.PageSize).ToList();
            }

            return data;
        }

        public GenralReportListRequest<ContactUsResponseModel> GetConatctusReports(GenralReportListRequest<ContactUsResponseModel> page, DateTime? str, DateTime? endd)
        {
            DateTime strt = Convert.ToDateTime(str);
            DateTime end = Convert.ToDateTime(endd).AddDays(1);
            GenralReportListRequest<ContactUsResponseModel> data = new GenralReportListRequest<ContactUsResponseModel>();
            DateTime datte = DateTime.UtcNow;
            using (var db = new AzzuhaDbContext())
            {
                var query = db
                    .ContactQueries.Where(x => x.CreatedOn >= strt && x.CreatedOn <= end)
               .Select(x => new ContactUsResponseModel
               {
                   Emailaddress=x.Email,
                   FirstName=x.FirstName,
                   LastName=x.LastName,
                   Message=x.Query,
                   PhoneNumber=x.PhoneNumber,
                   CreatedDtg=x.CreatedOnDate,
               })
               .OrderByDescending(x => x.CreatedDtg)
               .AsQueryable();

                if (!string.IsNullOrEmpty(page.Search))
                {
                    var date = new DateTime();
                    var sdate = DateTime.TryParse(page.Search, out date);
                    int number = -1;
                    var isNumber = Int32.TryParse(page.Search, out number);
                 
                }
                var orderedQuery = query.OrderByDescending(x => x.CreatedDtg);
                switch (page.SortIndex)
                {
                    case 0:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.FirstName) : query.OrderBy(x => x.FirstName);
                        break;
                    case 1:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.LastName) : query.OrderBy(x => x.LastName);
                        break;
                    case 2:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.Emailaddress) : query.OrderBy(x => x.Emailaddress);
                        break;
                    case 3:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.PhoneNumber) : query.OrderBy(x => x.PhoneNumber);
                        break;
                    case 4:
                        orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.CreatedDtg) : query.OrderBy(x => x.CreatedDtg);
                        break;
                  
                }


                data.Page = page.Page;
                data.PageSize = page.PageSize;
                data.TotalRecords = orderedQuery.Count();
                data.Draw = page.Draw;
                data.Entities = orderedQuery.Skip(page.Page).Take(page.PageSize).ToList();
            }

            return data;
        }
    }
}
