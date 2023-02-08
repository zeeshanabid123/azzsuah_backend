using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class Users
    {
        public Users()
        {
            SubscriptionUserInfos = new HashSet<SubscriptionUserInfos>();
            UserAddresses = new HashSet<UserAddresses>();
        }

        public Guid Id { get; set; }
        public string UniqueIdentifier { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? ProfileTypeId { get; set; }
        public bool? IsNewsletter { get; set; }
        public bool? IsSubscribed { get; set; }
        public string StripeCustomerId { get; set; }
        public Guid? ForgotLinkKey { get; set; }
        public DateTime? ForgotExpiry { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsUsed { get; set; }
        public string QuickBookCustumerId { get; set; }

        public virtual ICollection<SubscriptionUserInfos> SubscriptionUserInfos { get; set; }
        public virtual ICollection<UserAddresses> UserAddresses { get; set; }
    }
}
