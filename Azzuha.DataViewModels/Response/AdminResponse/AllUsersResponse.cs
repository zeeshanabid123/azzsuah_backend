using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
  public  class AllUsersResponse
    {
        public string Name { get; set; }
        public string ProfileTypeName { get; set; }
        public string Email { get; set; }
        public DateTime SignUpdate { get; set; }
        public bool? Status { get; set; }
    }
}
