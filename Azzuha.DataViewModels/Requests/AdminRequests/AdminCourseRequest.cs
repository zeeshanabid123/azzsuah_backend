using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Requests.AdminRequests
{
   public class AdminCourseRequest
    {
        public Guid Id { get; set; }
        public string CourseImage { get; set; }
        public int? CourseDuration { get; set; }
        public string CourseType { get; set; }
        public string CourseDetail { get; set; }
        public string CourseName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
