using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class RoleSrights
    {
        public RoleSrights()
        {
            AssignedRights = new HashSet<AssignedRights>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? Grade { get; set; }
        public string Logo { get; set; }
        public long? FkSelf { get; set; }
        public bool? EnableFlag { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<AssignedRights> AssignedRights { get; set; }
    }
}
