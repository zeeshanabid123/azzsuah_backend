using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
    public class VisitCountChartResponse
    {
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }
}
