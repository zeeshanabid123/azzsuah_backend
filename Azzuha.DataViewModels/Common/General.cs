using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class General<T>
    {
        [JsonProperty(PropertyName = "id")]
        public T Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tooltip")]
        public string Tooltip { get; set; } = "";

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; } = 0;
    }
}
