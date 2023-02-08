using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
    public class UnSubscribedResponse
    {
        public Guid UserId { get; set; }
        public Guid ProfileTypeId { get; set; }
        public DateTime SignupDate { get; set; }
        public DateTime CancelledDate { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public double Fee { get; set; }
        public string SubscriptionTerm { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
