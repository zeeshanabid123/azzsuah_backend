using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests.AdminRequests
{
    public class EventAdminRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Guid? EventTypeId { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
