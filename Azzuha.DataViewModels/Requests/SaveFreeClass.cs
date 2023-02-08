using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
    public class SaveFreeClass<T>
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "video")]
        public string Video { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public decimal Duration { get; set; }

        [JsonProperty(PropertyName = "hashtags")]
        public List<string> Hashtags { get; set; }

        [JsonProperty(PropertyName = "libraryId")]
        public Guid LibraryId { get; set; } = Guid.Empty;

        [JsonProperty(PropertyName = "isLike")]
        public bool? IsLike { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public List<T> Categories { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
