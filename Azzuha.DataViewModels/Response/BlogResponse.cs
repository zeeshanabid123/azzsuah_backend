using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
    public class BlogResponse
    {
        private Guid _id;
        [Key]
        public Guid ID
        {
            get { return _id; }
            set { _id = Guid.NewGuid(); }
        }

        [JsonProperty(PropertyName = "blogId")]
        public Guid BlogId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "slugUrl")]
        public string SlugUrl { get; set; }

        [JsonProperty(PropertyName = "metaTag")]
        public string MetaTag { get; set; }

        [JsonProperty(PropertyName = "metaDescription")]
        public string MetaDescription { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "imageThumbnailUrl")]
        public string ImageThumbnailUrl { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isLiked")]
        public bool IsLiked { get; set; }

        [JsonProperty(PropertyName = "visits")]
        public int Visits { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
    }
}
