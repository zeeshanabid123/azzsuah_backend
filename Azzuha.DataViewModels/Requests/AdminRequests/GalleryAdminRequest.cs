using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests.AdminRequests
{
   public class GalleryAdminRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public List<AdminGalleryImageRequest> GalleryImagesmodel { get; set; }
    }
}
