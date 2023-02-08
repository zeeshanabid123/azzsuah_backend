using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
  public  class MediaResponseModel
    {
        public Guid Id { get; set; }
        public Guid? MediaTypeId { get; set; }
        public bool? IsAudio { get; set; }
        public string FileUrl { get; set; }
        public string Name { get; set; }
    }
}
