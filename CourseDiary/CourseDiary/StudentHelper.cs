using Course.Infrastructure;
using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary
{
    public class StudentHelper
    {
        private readonly StudentService _studentService;
        private readonly CliHelper _cliHelper;

        public StudentHelper()
        {
            var studentRepository = new StudentRepository();

            _studentService = new StudentService(studentRepository);
            _cliHelper = new CliHelper();
        }

        public async Task AddStudentAsync()
        {
            Console.Clear();
            List<Student> students = await _studentService.GetAllStudentsAsync();

            Student student = new Student();
            var success = false;

            student.Name = _cliHelper.GetStringFromUser("Name: ");
            student.Surname = _cliHelper.GetStringFromUser("Surname: ");
            student.Password = _cliHelper.GetStringFromUser("Password: ");
            do
            {
                student.Email = _cliHelper.GetStringFromUser("Email: ");
                if (students
                    .Where(x => x.Email == student.Email)
                    .Count() == 0)
                {
                    success = true;
                }
                else 
                { 
                    Console.WriteLine("We have that email in base, please type another email address");
                    success = false;
                }
            }while(success);
                       
            student.BirthDate = _cliHelper.GetDateFromUser("Birth Date: ");
            

            await _studentService.AddStudentAsync(student);
        }

    }
}
