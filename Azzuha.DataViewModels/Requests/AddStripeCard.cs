using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class AddStripeCard
    {
        [JsonProperty(PropertyName = "stripeToken")]
        public string StripeToken { get; set; }

        [JsonProperty(PropertyName = "cardHolderName")]
        public string CardHolderName { get; set; }

        [JsonProperty(PropertyName = "address")]
        public AddressModel Address { get; set; }
    }
}
