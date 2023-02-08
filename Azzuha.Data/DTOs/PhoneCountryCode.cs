using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class PhoneCountryCode
    {
        public long Id { get; set; }
        public string CountryCode { get; set; }
        public string DialCode { get; set; }
        public string CountryName { get; set; }
        public string LowResulationImageUrl1 { get; set; }
        public string HighResulationImageUrl { get; set; }
    }
}
