using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
  public  class AdminEventResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "imageUrl")]

        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "eventTypeId")]

        public Guid? EventTypeId { get; set; }
        [JsonProperty(PropertyName = "isEnabled")]

        public bool IsEnabled { get; set; }
      
    }
}
