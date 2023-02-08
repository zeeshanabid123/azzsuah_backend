using System;
using System.Collections.Generic;

namespace Azzuha.Data.DTOs
{
    public partial class Courses1
    {
        public Guid Id { get; set; }
        public string CourseImage { get; set; }
        public int? CourseDuration { get; set; }
        public string CourseType { get; set; }
        public string CourseDetail { get; set; }
        public string CourseName { get; set; }
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
