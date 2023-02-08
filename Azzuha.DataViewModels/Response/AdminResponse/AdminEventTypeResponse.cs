using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
   public class AdminEventTypeResponse
    {
        public Guid Id { get; set; }
        public string EventImage { get; set; }
        public string EventName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
