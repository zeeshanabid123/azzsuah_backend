using Azzuha.DataViewModels.Requests.AdminRequests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
   public class GalleryResponseModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "name")]

        public string Name { get; set; }
        [JsonProperty(PropertyName = "isEnabled")]

        public bool IsEnabled { get; set; }
        [JsonProperty(PropertyName = "galleryImagesmodel")]

        public List<AdminGalleryImageRequest> GalleryImagesmodel { get; set; }
    }
}
