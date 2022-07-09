using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Models
{
    public class CourseRate
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Description { get; set; }
        public float ProgramRate { get; set; }
        public float TrainerRate { get; set; }
        public float ToolsRate { get; set; }
    }
}
