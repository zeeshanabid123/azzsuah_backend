using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
  public  class AdminBlogResponse
    {
      

        [JsonProperty(PropertyName = "blogId")]
        public Guid BlogId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "imageThumbnailUrl")]
        public string ImageThumbnailUrl { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "IsEnabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        public string metaTitle { get; set; }
        public string metaDescription { get; set; }
        public string slugUrl { get; set; }

    }
}
