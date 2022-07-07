using System;

namespace CourseDiary.Domain.Models
{
    public enum Presence
    {
        Present,
        Absent,
        Justified
    }

    public class StudentPresence
    {
        public DateTime LessonDate;
        public Student Student;
        public Course Course;
        public Presence Presence;
    }
}
