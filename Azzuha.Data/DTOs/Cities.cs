using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Azzuha.Data.DTOs
{
    public partial class Cities
    {
        public Cities()
        {
            UserAddresses = new HashSet<UserAddresses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public string StateCode { get; set; }
        public int? CountryId { get; set; }
        public string CountryCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Geometry GeographyPoint { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool? Flag { get; set; }
        public string WikiDataId { get; set; }

        public virtual Countries Country { get; set; }
        public virtual States State { get; set; }
        public virtual ICollection<UserAddresses> UserAddresses { get; set; }
    }
}
