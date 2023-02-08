using Azzuha.DataViewModels.Response.AdminResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azzuha.AdminPanel.Models
{
    public class PaginationModel
    {
        public int Draw { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int SortIndex { get; set; } = -1;
        public string Search { get; set; }
        public string SortByFieldName { get; set; }
        public string SortBy { get; set; }
        public List<AdminBlogResponse> Entities { get; set; }
    }
   
}
