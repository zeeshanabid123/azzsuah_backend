using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsVerified { get; set; }
        public bool? IsBlock { get; set; }
        public int NumberOfContact { get; set; }
    }
}
