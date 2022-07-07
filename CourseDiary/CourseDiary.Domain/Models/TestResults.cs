using System;

namespace CourseDiary.Domain.Models
{
    public class TestResults
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public DateTime FinishDate { get; set; }
        public int StudentId { get; set; }
        public float Result { get; set; }
    }
}
