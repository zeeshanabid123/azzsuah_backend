using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class SubscriptionUserInfos
    {
        public Guid Id { get; set; }
        public string StripCode { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid ProfilePackId { get; set; }
        public Guid UserId { get; set; }
        public bool? IsSubscribed { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public bool IsEnabled { get; set; }

        public virtual Users User { get; set; }
    }
}
