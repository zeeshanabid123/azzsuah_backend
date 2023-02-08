using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class ContactQueries
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Query { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
