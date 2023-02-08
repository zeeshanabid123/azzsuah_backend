using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
   public class AdminMarketingResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }

        public DateTime CreatedDtg { get; set; }
        public string ThumbnailUrl { get; set; }
        public Guid? ProfileTypeId { get; set; }
        public string LibraryName { get; set; }
        public string ProfileTypeName { get; set; }
        public bool IsUsed { get; set; }
        public int Pid { get; set; }
        public string videoUrl { get; set; }
        public Guid TableId { get; set; }
    }

    public class ProfileTypeViewModel
    {
        public Guid profileTypeId { get; set; }
        public string Name { get; set; }

    }

   public class FreeClassCategoriesViewModel
    {
        public Guid profileTypeId { get; set; }
        public string Name { get; set; }
    }


}
