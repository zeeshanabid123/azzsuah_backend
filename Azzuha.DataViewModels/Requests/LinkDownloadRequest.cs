using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class LinkDownloadRequest
    {
        [JsonProperty(PropertyName = "trainerId")]
        public Guid TrainerId { get; set; }

        [JsonProperty(PropertyName = "deviceTypeId")]
        public int DeviceTypeId { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "profileTypeId")]
        public Guid ProfileTypeId { get; set; }
    }
}
