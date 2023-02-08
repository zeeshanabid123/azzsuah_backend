using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
   public class AdminSliderResponse
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
    }
}
