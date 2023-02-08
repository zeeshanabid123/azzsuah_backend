using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class Location
    {
        [JsonProperty(PropertyName = "location")]
        public string Loc { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "isPublic")]
        public bool IsPublic { get; set; }
    }
}
