using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
   public class AdminResponseViewModel
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FatherName { get; set; }
        public string Dob { get; set; }
        public string IdCardNumber { get; set; }
        public string CurrentAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PrevoiusEducation { get; set; }
        public string PeducationFrom { get; set; }
        public int? CourseTypeId { get; set; }
        public string SchoolRecordUrl { get; set; }
        public string Cnicurl { get; set; }
        public string BformUrl { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
