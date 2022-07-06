using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Models
{
    public class StudentInCourse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set;}
        public string StudentSurname { get; set;}
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public State CourseState { get; set; }
    }
}
