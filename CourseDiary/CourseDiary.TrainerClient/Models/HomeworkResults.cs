using System;

namespace CourseDiary.TrainerClient.Models
{
    public class HomeworkResults
    {
        public int Id { get; set; }
        public string HomeworkName { get; set; }
        public DateTime FinishDate { get; set; }
        public int StudentId { get; set; }
        public float Result { get; set; }

    }
}
