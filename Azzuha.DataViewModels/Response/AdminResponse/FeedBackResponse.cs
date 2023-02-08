using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
    public class FeedBackResponse
    {
        public Guid UserId { get; set; }
        public Guid ProfileTypeId { get; set; }
        public string Name { get; set; }
        public Guid? BlogId { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
        public bool IsBlogSuggestion { get; set; }
        public DateTime Date { get; set; }
    }
}
