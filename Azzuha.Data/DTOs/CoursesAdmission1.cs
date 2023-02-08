using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class CoursesAdmission1
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
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
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
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
