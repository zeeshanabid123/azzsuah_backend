using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Azzuha.Data.DTOs
{
    public partial class UserAddresses
    {
        public Guid Id { get; set; }
        public Guid ProfileTypeId { get; set; }
        public int AddressTypeId { get; set; }
        public Guid UserId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string AddressLatitude { get; set; }
        public string AddressLongitude { get; set; }
        public Geometry AddressGeographyPoint { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public Geometry LocationGeographyPoint { get; set; }
        public string ZipCode { get; set; }
        public bool IsOnline { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }

        public virtual Cities City { get; set; }
        public virtual Countries Country { get; set; }
        public virtual States State { get; set; }
        public virtual Users User { get; set; }
    }
}
