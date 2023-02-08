using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class States
    {
        public States()
        {
            Cities = new HashSet<Cities>();
            UserAddresses = new HashSet<UserAddresses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public string CountryCode { get; set; }
        public string FipsCode { get; set; }
        public string Iso2 { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? Flag { get; set; }
        public string WikiDataId { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
        public virtual ICollection<UserAddresses> UserAddresses { get; set; }
    }
}
