using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class MediaTypes
    {
        public MediaTypes()
        {
            Media = new HashSet<Media>();
        }

        public Guid Id { get; set; }
        public string MediaTypeName { get; set; }
        public string MediaImage { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }

        public virtual ICollection<Media> Media { get; set; }
    }
}
