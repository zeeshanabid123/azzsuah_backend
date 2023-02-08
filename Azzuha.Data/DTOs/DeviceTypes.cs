using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class DeviceTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public bool IsEnabled { get; set; }
    }
}
