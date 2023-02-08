using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response
{
  public  class JsonResponseModel
    {
        public Guid Id { get; set; }
        public string DataHeadingJson { get; set; }
        public bool IsEnabled { get; set; }
    }
}
