using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests.AdminRequests
{
  public  class AdminJsonEditorRequest
    {
        public Guid Id { get; set; }
        public string DataHeadingJson { get; set; }
        public bool IsEnabled { get; set; }
    }
}
