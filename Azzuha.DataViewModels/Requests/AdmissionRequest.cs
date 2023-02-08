using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests
{
  public  class AdmissionRequest
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
    }
}
