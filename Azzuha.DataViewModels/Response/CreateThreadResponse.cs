using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
    public class CreateThreadResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
