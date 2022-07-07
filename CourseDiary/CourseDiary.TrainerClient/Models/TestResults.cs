using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.TrainerClient.Models
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
