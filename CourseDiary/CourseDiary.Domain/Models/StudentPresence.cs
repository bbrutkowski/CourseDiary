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
        public int StudentId;
        public int CourseId;
        public Presence Presence;
    }
}
