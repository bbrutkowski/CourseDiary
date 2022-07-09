using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.TrainerClient.Models
{
    public class CourseResults
    {
        public int Id;
        public Course Course;
        public List<StudentResult> StudentResults;
    }
}
