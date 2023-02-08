using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
  public class RightsViewModel
    {
        public bool DashBorad { get; set; }
        public bool UserManager { get; set; }
        public bool AdminBlog { get; set; }
        public bool MarketingLibaray { get; set; }
        public bool Voucher { get; set; }
        public bool Report { get; set; }
        public bool Profile { get; set; }
        public bool EmailSubscription { get; set; }


    }
}
