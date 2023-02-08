using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class GenralReportListRequest<T>
    {
        public int Draw { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int SortIndex { get; set; } = -1;
        public string Search { get; set; }
        public string SortByFieldName { get; set; }
        public string SortBy { get; set; }
        public List<T> Entities { get; set; }
    }
}
