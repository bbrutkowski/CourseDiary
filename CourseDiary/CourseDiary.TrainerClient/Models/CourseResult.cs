using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.TrainerClient.Models
{
    public class CourseResult
    {
        public StudentPresence StudentPresence { get; set; }
        public HomeworkResults HomeworkResults { get; set; }
        public TestResults TestResults { get; set; }
        public Student Student { get; set; }
        public bool isComplited { get; set; }
    }
}
