using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests.AdminRequests
{
    public class CreateVoucherRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime Expiry { get; set; }
        public int ExpiryMonth { get; set; }
        public decimal Discount { get; set; }
        public Guid Id { get; set; }
    }
}
