using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class GetStatsRequest
    {
        [Required]
        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [JsonProperty(PropertyName = "endDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [JsonProperty(PropertyName = "frequency")]
        public int Frequency { get; set; }
    }
}
