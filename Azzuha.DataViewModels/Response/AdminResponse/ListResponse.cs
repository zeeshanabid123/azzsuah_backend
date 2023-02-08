using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
    public class ListResponse
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public List<string> Location { get; set; }
        public List<string> List1 { get; set; }
        public List<string> List2 { get; set; }
        public decimal AverageHourlyPrice { get; set; }
        public int NumberOfMarketingMaterials { get; set; }
        public int NumberOfViews { get; set; }
        public int NumberOfMessages { get; set; }
        public int NumberOfLikes { get; set; }
        public double Fee { get; set; }
        public string SubscriptionTerm { get; set; }
    }
}