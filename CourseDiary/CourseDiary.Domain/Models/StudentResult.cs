using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Models
{
    public enum FinalResult
    {
        Passed,
        Failed
    }

    public class StudentResult
    {
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public float StudentPresencePercentage { get; set; }
        public float StudentJustifiedAbsencePercentage { get; set; }
        public float StudentHomeworkPercentage { get; set; }
        public float StudentTestPercentage { get; set; }
        public FinalResult FinalResult { get; set; }
    }
}
