using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Azzuha.Data.Context;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Requests;
using Azzuha.Services.Interface.admin;
using Azzuha.Data.DTOs;

namespace Azzuha.Services.Implementation.admin
{
    public class EmailSubscriptionAdminService: IEmailSubscriptionAdminService
    {
        //public GenralReportListRequest<EmailSubscriptionSaveResponse> Get(GenralReportListRequest<EmailSubscriptionSaveResponse> page, DateTime? str, DateTime? endd)
        //{
        //    DateTime strt = Convert.ToDateTime(str);
        //    DateTime end = Convert.ToDateTime(endd).AddDays(1);
        //    GenralReportListRequest<EmailSubscriptionSaveResponse> data = new GenralReportListRequest<EmailSubscriptionSaveResponse>();

        //    using (var db = new AzzuhaDbContext())
        //    {
        //        var query = db
        //            .EmailPrivacyFeedbacks
        //            .Where(x => x.CreatedOn >= strt && x.CreatedOn <= end)
        //       .Select(x => new EmailSubscriptionSaveResponse
        //       {
        //           Id = x.Id,
        //           MessagesAndFollowers = x.MessagesAndFollowers ?? false,
        //           MembershipAndInvoice = x.MembershipAndInvoice ?? false,
        //           MonthlyReports = x.MonthlyReports ?? false,
        //           Marketing = x.Marketing ?? false,
        //           UnsubscribeFromAll = x.UnsubscribeFromAll ?? false,
        //           Blogs = x.Blogs,
        //           Email = x.Email,
        //           CreatedOn = x.CreatedOn
        //       })
        //       .AsQueryable();

        //        if (!string.IsNullOrEmpty(page.Search))
        //        {
        //            var date = new DateTime();
        //            var sdate = DateTime.TryParse(page.Search, out date);
        //            int number = -1;
        //            var isNumber = Int32.TryParse(page.Search, out number);
        //            if (sdate)
        //                query = query.Where(
        //                    x => x.CreatedOn.Date == date.Date
        //                );
        //            else if (isNumber)
        //            {
        //                //query = query.Where(
        //                //    x => x.Fee == number
        //                //);
        //            }
        //            else
        //            {
        //                query = query.Where(
        //                x => x.Email.ToLower().Contains(page.Search.ToLower())
        //            );
        //            }
        //        }
        //        var orderedQuery = query.OrderByDescending(x => x.CreatedOn);
        //        switch (page.SortIndex)
        //        {
        //            case 0:
        //                orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.Email) : query.OrderBy(x => x.Email);
        //                break;
        //            case 1:
        //                orderedQuery = page.SortBy == "desc" ? query.OrderByDescending(x => x.CreatedOn) : query.OrderBy(x => x.CreatedOn);
        //                break;

        //        }


        //        data.Page = page.Page;
        //        data.PageSize = page.PageSize;
        //        data.TotalRecords = orderedQuery.Count();
        //        data.Draw = page.Draw;
        //        data.Entities = orderedQuery.Skip(page.Page).Take(page.PageSize).ToList();
        //    }

        //    return data;
        //}
    }
}
