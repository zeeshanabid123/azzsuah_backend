using Azzuha.DataViewModels.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
    public class GymProfileResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "amenities")]
        public List<string> Amenities { get; set; }

        [JsonProperty(PropertyName = "otherAmenities")]
        public List<string> OtherAmenities { get; set; }

        [JsonProperty(PropertyName = "personal")]
        public FitnessGymInformationResponse Personal { get; set; }

        [JsonProperty(PropertyName = "location")]
        public List<LocationResponse> Location { get; set; }

        [JsonProperty(PropertyName = "price")]
        public List<FitnessGymPricingModelResponse> Price { get; set; }

        [JsonProperty(PropertyName = "calender")]
        public List<FitnessGymCalendarModelResponse> Calender { get; set; }

        [JsonProperty(PropertyName = "freeClass")]
        public List<SaveFreeClass<string>> FreeClasses { get; set; }
    }

    public class FitnessGymInformationResponse
    {
        [JsonProperty(PropertyName = "gymName")]
        public string GymName { get; set; }

        [JsonProperty(PropertyName = "contactName")]
        public string ContactName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "businessLegalName")]
        public string BusinessLegalName { get; set; }

        [JsonProperty(PropertyName = "websiteUrl")]
        public string WebsiteUrl { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "isLike")]
        public bool IsLike { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public decimal Rating { get; set; }

        [JsonProperty(PropertyName = "ratingCount")]
        public int ratingCount { get; set; }

        [JsonProperty(PropertyName = "isIRated")]
        public bool IsIRated { get; set; }
    }

    public class LocationResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "profileTypeId")]
        public Guid ProfileTypeId { get; set; }

        [JsonProperty(PropertyName = "addressTypeId")]
        public int AddressTypeId { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

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

    public class FitnessGymPricingModelResponse
    {
        [JsonProperty(PropertyName = "frequency")]
        public string Frequency { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
    }

    public class FitnessGymCalendarModelResponse
    {
        [JsonProperty(PropertyName = "Day")]
        public string Day { get; set; }

        [JsonProperty(PropertyName = "StartTime")]
        public string? StartTime { get; set; }

        [JsonProperty(PropertyName = "EndTime")]
        public string? EndTime { get; set; }

        [JsonProperty(PropertyName = "is24Hours")]
        public bool Is24Hours { get; set; }

        [JsonProperty(PropertyName = "GMT")]
        public string GMT { get; set; }
    }
}
