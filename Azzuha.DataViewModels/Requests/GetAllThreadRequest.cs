using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class GetAllThreadRequest
    {
        [JsonProperty(PropertyName = "search")]
        public string Search { get; set; }

        [JsonProperty(PropertyName = "skip")]
        public int Skip { get; set; }

        [JsonProperty(PropertyName = "take")]
        public int Take { get; set; }
    }
}
