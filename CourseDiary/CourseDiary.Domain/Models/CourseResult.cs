namespace CourseDiary.Domain.Models
{
    public  class CourseResult
    {
        public StudentPresence StudentPresence { get; set; }
        public HomeworkResults HomeworkResults { get; set; }
        public TestResults TestResults { get; set; }
        public bool isComplited { get; set; }
        public Student Student { get; set; }
    }
}
