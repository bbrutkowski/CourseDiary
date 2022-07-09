using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Models
{
    public class HomeworkResults
    {
        public int Id { get; set; }
        public string HomeworkName { get; set; }
        public DateTime FinishDate { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public float Result { get; set; }
    }
}
