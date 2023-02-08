using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class UploadDocuments
    {
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; } = 0;
    }
}
