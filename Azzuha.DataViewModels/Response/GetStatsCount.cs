using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
    public class GetStatsCount
    {
        private Guid _id;
        [Key]
        public Guid ID
        {
            get { return _id; }
            set { _id = Guid.NewGuid(); }
        }

        [JsonProperty(PropertyName = "presentStats")]
        public decimal PresentStats { get; set; }

        [JsonProperty(PropertyName = "pastStats")]
        public decimal PastStats { get; set; }

        [JsonProperty(PropertyName = "difference")]
        public decimal Difference { get; set; }
    }
}
