using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class Countries
    {
        public Countries()
        {
            Cities = new HashSet<Cities>();
            States = new HashSet<States>();
            UserAddresses = new HashSet<UserAddresses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public string Iso2 { get; set; }
        public string PhoneCode { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string Native { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Flag { get; set; }
        public string WikiDataId { get; set; }

        public virtual ICollection<Cities> Cities { get; set; }
        public virtual ICollection<States> States { get; set; }
        public virtual ICollection<UserAddresses> UserAddresses { get; set; }
    }
}
