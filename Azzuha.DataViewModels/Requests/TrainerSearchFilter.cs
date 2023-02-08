using Azzuha.DataViewModels.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class TrainerSearchFilter
    {
        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "radius")]
        public int Radius { get; set; }

        [JsonProperty(PropertyName = "searchKeyword")]
        public string SearchKeyword { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public List<Guid> Categories { get; set; }

        [JsonProperty(PropertyName = "facilities")]
        public List<Guid> Facilities { get; set; }

        [JsonProperty(PropertyName = "amenities")]
        public List<Guid> Amenities { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public List<TimeRangeModel<decimal, decimal>> Duration { get; set; }

        [JsonProperty(PropertyName = "dietaryPreferences")]
        public List<Guid> DietaryPreferences { get; set; }

        [JsonProperty(PropertyName = "foodAllergies")]
        public List<Guid> FoodAllergies { get; set; }

        [JsonProperty(PropertyName = "isLibrary")]
        public bool IsLibrary { get; set; } = false;

        [JsonProperty(PropertyName = "days")]
        public List<int> Days { get; set; }

        [JsonProperty(PropertyName = "genderIds")]
        public List<int> GenderIds { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }

        [JsonProperty(PropertyName = "sortBy")]
        public int SortBy { get; set; }

        [JsonProperty(PropertyName = "skip")]
        public int Skip { get; set; }

        [JsonProperty(PropertyName = "take")]
        public int Take { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; } = 0;

        [JsonProperty(PropertyName = "is24Hour")]
        public int Is24Hour { get; set; } = -1;

        [JsonProperty(PropertyName = "times")]
        public List<TimeRangeModel<string, string>> Time { get; set; }

        [JsonProperty(PropertyName = "prices")]
        public List<RangeModel<decimal, decimal>> Prices { get; set; }

        [JsonProperty(PropertyName = "searchType")]
        public int SearchType { get; set; } = 0;
    }
}
