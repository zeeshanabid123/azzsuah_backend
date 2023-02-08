using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class AddToLibraryRequest
    {
        [JsonProperty(PropertyName = "libraryId")]
        public Guid LibraryId { get; set; }

        [JsonProperty(PropertyName = "classId")]
        public Guid ClassId { get; set; }
    }
}
