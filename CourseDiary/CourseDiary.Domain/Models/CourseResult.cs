using System.Collections.Generic;

namespace CourseDiary.Domain.Models
{
    public class CourseResult
    {
        public Course Course;
        public List<StudentPresence> StudentPresences;
        public List<HomeworkResults> HomeworkResults;
        public List<TestResults> TestResults;
    }
}
