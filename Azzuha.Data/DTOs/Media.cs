using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class Media
    {
        public Guid Id { get; set; }
        public Guid? MediaTypeId { get; set; }
        public bool? IsAudio { get; set; }
        public string FileUrl { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }

        public virtual MediaTypes MediaType { get; set; }
    }
}
