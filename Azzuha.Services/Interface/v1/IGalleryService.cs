using Azzuha.DataViewModels.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.Services.Interface.v1
{
  public  interface IGalleryService
    {
        public List<GalleryResponseModel> GetGallery();
    }
}
