using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class AddressModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "profileTypeId")]
        public Guid ProfileTypeId { get; set; }

        [JsonProperty(PropertyName = "addressTypeId")]
        public int AddressTypeId { get; set; }

        [JsonProperty(PropertyName = "countryId")]
        public int? CountryId { get; set; }

        [JsonProperty(PropertyName = "stateId")]
        public int? StateId { get; set; }

        [JsonProperty(PropertyName = "cityId")]
        public int? CityId { get; set; }

        [JsonProperty(PropertyName = "address1")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName = "address2")]
        public string Address2 { get; set; }

        [JsonProperty(PropertyName = "addressLatitude")]
        public string AddressLatitude { get; set; }

        [JsonProperty(PropertyName = "addressLongitude")]
        public string AddressLongitude { get; set; }

        [JsonProperty(PropertyName = "locationLatitude")]
        public string LocationLatitude { get; set; }

        [JsonProperty(PropertyName = "locationLongitude")]
        public string LocationLongitude { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "isOnline")]
        public bool IsOnline { get; set; }
    }
}
