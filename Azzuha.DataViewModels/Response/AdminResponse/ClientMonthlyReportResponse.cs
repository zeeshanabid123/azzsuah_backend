using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
    public class ClientMonthlyReportResponse
    {
        public Guid UserId { get; set; }
        public Guid ProfileTypeId { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public DateTime Month { get; set; }
        public DateTime Date { get; set; }
    }
}
