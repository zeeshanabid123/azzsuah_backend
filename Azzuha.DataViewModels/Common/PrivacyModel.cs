using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class PrivacyModel
    {
        [JsonProperty(PropertyName = "privacyTypeId")]
        public int PrivacyTypeId { get; set; }

        [JsonProperty(PropertyName = "isPublic")]
        public bool IsPublic { get; set; }
    }
}
