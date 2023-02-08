using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
    public class UserList
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int EnrolId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
        public string DailCode { get; set; }
        public bool? EnableFlag { get; set; }
        public DateTime? CreatedDtg { get; set; }
    }
}
