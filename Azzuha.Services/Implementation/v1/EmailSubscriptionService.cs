using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azzuha.Common;
using Azzuha.Data.Context;
using Azzuha.Data.DTOs;
using Azzuha.DataViewModels.Requests;
using Azzuha.Services.Interface.v1;
using Microsoft.EntityFrameworkCore;

namespace Azzuha.Services.Implementation.v1
{
    public class EmailSubscriptionService : IEmailSubscriptionService
    {
        //public async Task<bool> Save(EmailSubscriptionSaveRequest x)
        //{
        //    try
        //    {
        //        using (var db = new AzzuhaDbContext())
        //        {
        //            try
        //            {
        //                var findPreferences =
        //                    await db.EmailPrivacyFeedbacks.FirstOrDefaultAsync(y => y.Email == x.Email);

        //                if (findPreferences == null)
        //                {
        //                    await db.EmailPrivacyFeedbacks.AddAsync(new EmailPrivacyFeedbacks()
        //                    {
        //                        Id = SystemGlobal.GetId(),
        //                        IsEnabled = true,
        //                        CreatedBy = x.Email.ToString(),
        //                        CreatedOn = DateTime.UtcNow,
        //                        CreatedOnDate = DateTime.UtcNow,
        //                        Blogs = x.Blogs,
        //                        Email = x.Email,
        //                        Marketing = x.Marketing,
        //                        MembershipAndInvoice = x.MembershipAndInvoice,
        //                        MessagesAndFollowers = x.MessagesAndFollowers,
        //                        MonthlyReports = x.MonthlyReports,
        //                        UnsubscribeFromAll = x.UnsubscribeFromAll
        //                    });
        //                }
        //                else
        //                {
        //                    findPreferences.Blogs = x.Blogs;
        //                    findPreferences.Marketing = x.Marketing;
        //                    findPreferences.MembershipAndInvoice = x.MembershipAndInvoice;
        //                    findPreferences.MessagesAndFollowers = x.MessagesAndFollowers;
        //                    findPreferences.MonthlyReports = x.MonthlyReports;
        //                    findPreferences.UnsubscribeFromAll = x.UnsubscribeFromAll;
        //                    findPreferences.UpdatedBy = x.Email;
        //                    findPreferences.UpdatedOn = DateTime.UtcNow;
        //                }

        //                await db.SaveChangesAsync();

        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
