using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
   public class ContactUsRequestModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }
    }
}
