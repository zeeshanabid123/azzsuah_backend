using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
    public class InternalReportResponse
    {
        public Guid UserId { get; set; }
        public Guid ProfileTypeId { get; set; }
        public string Name { get; set; }
        public string ProfileType { get; set; }
        public string Email { get; set; }
        public DateTime SignupDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
