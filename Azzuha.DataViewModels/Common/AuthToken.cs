using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Common
{
    public class AuthToken
    {
        public Guid UserId { get; set; }
        public Guid ProfileTypeId { get; set; }
    }
}
