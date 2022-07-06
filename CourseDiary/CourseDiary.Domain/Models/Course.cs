using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Models
{
    public class Course
    {
        public int Id;
        public string Name;
        public DateTime BeginDate;
        public Trainer Trainer;
        public List<Student> Students;
        public double PresenceTreshold = 70.0;
        public double HomeworkTreshold = 70.0;
        public double TestTreshold = 70.0;
    }
}
