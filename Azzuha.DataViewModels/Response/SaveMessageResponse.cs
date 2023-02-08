using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
    public class SaveMessageResponse
    {
        [JsonProperty(PropertyName = "response")]
        public bool response { get; set; }
    }
}
