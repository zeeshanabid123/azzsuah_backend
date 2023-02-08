using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class ChangePassword
    {
        [Required]
        [JsonProperty(PropertyName = "forgotLinkKey")]
        public Guid ForgotLinkKey { get; set; }

        [Required]
        [JsonProperty(PropertyName = "newPassword")]
        public string NewPassword { get; set; }
    }
}
