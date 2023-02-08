using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
   public class UserCustomerQuickBooks
    {
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddr { get; set; }
    }
}
