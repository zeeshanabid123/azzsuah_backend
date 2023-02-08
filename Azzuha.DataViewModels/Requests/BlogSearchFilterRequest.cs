using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class BlogSearchFilterRequest
    {
        [JsonProperty(PropertyName = "searchKeyword")]
        public string SearchKeyword { get; set; }

        [JsonProperty(PropertyName = "sortBy")]
        public int SortBy { get; set; }

        [JsonProperty(PropertyName = "skip")]
        public int Skip { get; set; }

        [JsonProperty(PropertyName = "take")]
        public int Take { get; set; }
    }
}
