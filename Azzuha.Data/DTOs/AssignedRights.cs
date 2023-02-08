using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class AssignedRights
    {
        public long Id { get; set; }
        public string RoleId { get; set; }
        public long? RightsId { get; set; }

        public virtual RoleSrights Rights { get; set; }
    }
}
