using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests.AdminRequests
{
  public  class AdminMediaRequest
    {
        public Guid Id { get; set; }
        public Guid? MediaTypeId { get; set; }
        public bool? IsAudio { get; set; }
        public string FileUrl { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
