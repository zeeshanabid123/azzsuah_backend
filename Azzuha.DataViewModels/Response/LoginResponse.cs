using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
    public class LoginResponse
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "imageThumbnailUrl")]
        public string ImageThumbnailUrl { get; set; }

        [JsonProperty(PropertyName = "profileTypeId")]
        public Guid ProfileTypeId { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "isSubscribed")]
        public bool IsSubscribed { get; set; }
    }
}
