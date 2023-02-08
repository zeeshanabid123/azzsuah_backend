using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Azzuha.DataViewModels.Response.AdminResponse
{
   public class AdminCourseResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "courseImage")]
        public string CourseImage { get; set; }
        [JsonProperty(PropertyName = "courseDuration")]
        public int? CourseDuration { get; set; }
        [JsonProperty(PropertyName = "courseType")]
        public string CourseType { get; set; }
        [JsonProperty(PropertyName = "courseDetail")]
        public string CourseDetail { get; set; }
        [JsonProperty(PropertyName = "courseName")]
        public string CourseName { get; set; }
        [JsonProperty(PropertyName = "isEnabled")]
        public bool IsEnabled { get; set; }
    }
}
