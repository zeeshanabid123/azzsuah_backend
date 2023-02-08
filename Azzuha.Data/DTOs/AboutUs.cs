using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class AboutUs
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
