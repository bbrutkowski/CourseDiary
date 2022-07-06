using CourseDiary.AdminClient.Clients;
using CourseDiary.AdminClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseDiary.AdminClient
{
    public class AdminClientActionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly StudentWebApiClient _studentWebApiClient;
        private readonly TrainerWebApiClient _trainerWebApiClient;
        private readonly CourseWebApiClient _courseWebApiClient;

        public AdminClientActionHandler()
        {
            _cliHelper = new CliHelper();
            _studentWebApiClient = new StudentWebApiClient();
            _trainerWebApiClient = new TrainerWebApiClient();
            _courseWebApiClient = new CourseWebApiClient();
        }

        public async void AddStudent()
        {
            Console.Clear();
            List<Student> students = await _studentWebApiClient.GetAllStudents();

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
            } while (success);

            student.BirthDate = _cliHelper.GetDateFromUser("Birth Date: ");


            await _studentWebApiClient.AddStudent(student);
        }

        public async void AddTrainer()
        {

            Trainer newTrainer = new Trainer
            {
                Name = _cliHelper.GetStringFromUser("Name: "),
                Surname = _cliHelper.GetStringFromUser("Surname: "),
                Password = _cliHelper.GetStringFromUser("Password: "),
                Email = _cliHelper.GetStringFromUser("Email: "),
                DateOfBirth = _cliHelper.GetDateFromUser("Date of birth(dd/mm/yyyy): ")
            };

            var isAdded = await _trainerWebApiClient.AddTrainer(newTrainer);
            if (isAdded == true)
            {
                Console.WriteLine("Trainer added successfully");
            }
        }

        public async void AddCourse()
        {
            List<Trainer> trainers = await _trainerWebApiClient.GetAllTrainers();
            List<Student> students = await _studentWebApiClient.GetAllStudents();
            Course newCourse = new Course();
            newCourse.Name = _cliHelper.GetStringFromUser("Course name: ");
            newCourse.BeginDate = _cliHelper.GetDateFromUser("Begin date(dd/mm/yyyy): ");
            foreach(var trainer in trainers)
            {
                Console.WriteLine($"{trainer.Id}. {trainer.Name} {trainer.Surname} - {trainer.Email}");
            }
            newCourse.Trainer = trainers.Where(x => x.Id == _cliHelper.GetIntFromUser("Choose id of trainer: ")).ToList()[0];
            foreach(var student in students)
            {
                Console.WriteLine($"{student.Id}. {student.Name} {student.Surname} - {student.Email}");
            }
            var idList = _cliHelper.GetStudentIds();
            newCourse.Students = students.Where(x => idList.Contains(x.Id)).ToList();

            await _courseWebApiClient.AddCourse(newCourse);
        }
    }
}
