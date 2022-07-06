using System.ComponentModel.DataAnnotations;

namespace CourseDiary.AdminClient.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
    }
}
