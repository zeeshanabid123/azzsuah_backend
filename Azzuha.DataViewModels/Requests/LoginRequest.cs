using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class LoginRequest
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
