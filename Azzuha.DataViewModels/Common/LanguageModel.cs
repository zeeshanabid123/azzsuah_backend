using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class LanguageModel<T>
    {
        [JsonProperty(PropertyName = "id")]
        public T Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
