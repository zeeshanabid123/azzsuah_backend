using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
   public class ContactUsResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Emailaddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDtg { get; set; }
    }
}
