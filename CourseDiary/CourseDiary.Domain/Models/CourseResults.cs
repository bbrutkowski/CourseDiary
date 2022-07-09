using System.Collections.Generic;

namespace CourseDiary.Domain.Models
{
    public class CourseResults
    {
        public int Id;
        public Course Course;
        public List<StudentResult> StudentResults;
    }
}
