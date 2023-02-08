using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class RangeModel<T1, T2>
    {
        [JsonProperty(PropertyName = "start")]
        public T1 Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public T1 End { get; set; }
    }

    public class TimeRangeModel<T1, T2> : RangeModel<T1, T2>
    {
        [JsonProperty(PropertyName = "isOpen")]
        public bool IsOpen { get; set; }
    }
}
