using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class SaveMessageRequest
    {
        [JsonProperty(PropertyName = "friend")]
        public Guid Friend { get; set; }

        [JsonProperty(PropertyName = "messageThreadId")]
        public Guid MessageThreadId { get; set; }

        [JsonProperty(PropertyName = "messageTypeId")]
        public int MessageTypeId { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
