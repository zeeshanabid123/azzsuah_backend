using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class AdminSystemConfigurations
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModfityBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string QbookAccessToken { get; set; }
    }
}
