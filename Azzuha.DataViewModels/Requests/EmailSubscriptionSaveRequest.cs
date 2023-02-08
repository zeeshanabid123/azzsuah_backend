using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class EmailSubscriptionSaveRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool Blogs { get; set; }
        public bool? Marketing { get; set; }
        public bool? MessagesAndFollowers { get; set; }
        public bool? MembershipAndInvoice { get; set; }
        public bool? MonthlyReports { get; set; }
        public bool? UnsubscribeFromAll { get; set; }
    }

    public class EmailSubscriptionSaveResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool? Blogs { get; set; }
        public bool? Marketing { get; set; }
        public bool? MessagesAndFollowers { get; set; }
        public bool? MembershipAndInvoice { get; set; }
        public bool? MonthlyReports { get; set; }
        public bool? UnsubscribeFromAll { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
