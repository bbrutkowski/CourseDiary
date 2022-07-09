using CourseDiary.TrainerClient.Clients;
using CourseDiary.TrainerClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseDiary.TrainerClient
{
    public class TrainerClientActionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly TrainerClientLoginHandler _trainerClientLoginHandler;
        private readonly CourseWebApiClient _courseWebApiClient;
        private readonly StudentWebApiClient _studentWebApiClient;
        private Trainer _loggedTrainer = null;
        private Course _selectedCourse;

        public TrainerClientActionHandler()
        {
            _cliHelper = new CliHelper();
            _trainerClientLoginHandler = new TrainerClientLoginHandler();
            _courseWebApiClient = new CourseWebApiClient();
            _studentWebApiClient = new StudentWebApiClient();
        }

        public bool ProgramLoop(Trainer loggedTrainer)
        {
            _loggedTrainer = loggedTrainer;
            bool exit = false;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("");

                switch (operation)
                {
                    case "1":
                        SelectActiveCourse();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "10":
                        _trainerClientLoginHandler.LoginLoop();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }
            return exit;
        }

        private async void SelectActiveCourse()
        {
            var allActiveCourses = await _courseWebApiClient.GetAllActiveCourses();
            foreach (Course course in allActiveCourses)
            {
                Console.WriteLine($"{course.Id}. {course.Name} - {course.Trainer.Name} {course.Trainer.Surname} - {course.BeginDate}");
            }
            var selectedId = _cliHelper.GetIntFromUser("Enter id of course you want to choose: ");
            _selectedCourse = allActiveCourses.Where(x => x.Id == selectedId).ToList()[0];
            MenuForActiveCourse();
        }

        private void MenuForActiveCourse()
        {
            var switchOption = _cliHelper.GetStringFromUser("What do you want to do?");
            var exit = false;
            while (!exit)
            {
                switch (switchOption)
                {
                    case "1":
                        ShowSelectedCourse();
                        break;
                    case "2":
                        AddPresence();
                        break;
                    case "3":
                        AddHomeworkResults();
                        break;
                    case "4":
                        AddTestResultsAsync();
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "10":
                        ProgramLoop(_loggedTrainer);
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unsupported option");
                        break;
                }

            }
        }

        private async void AddTestResultsAsync()
        {
            TestResults newTestResult = new TestResults()
            {
                TestName = _cliHelper.GetStringFromUser("Enter test name"),
                FinishDate = _cliHelper.GetDateFromUser("Enter end date of test"),
                StudentId = _cliHelper.GetIntFromUser("Enter student id"),
                CourseId = _selectedCourse.Id,
                Result = _cliHelper.GetIntFromUser("Enter result of test (0-100)"),
            };
            if (newTestResult.Result > 100)
            {
                Console.WriteLine("Value cannot be higher than 100");
            }
            else
            {
                await _courseWebApiClient.AddTestResult(newTestResult);
            }
        }

        private async void AddHomeworkResults()
        {
            HomeworkResults newResults = new HomeworkResults()
            {
                HomeworkName = _cliHelper.GetStringFromUser("Enter homework name"),
                FinishDate = _cliHelper.GetDateFromUser("Enter homework deadline"),
                StudentId = _cliHelper.GetIntFromUser("Enter student Id"),
                CourseId = _selectedCourse.Id,
                Result = float.Parse(_cliHelper.GetStringFromUser("Enter homework result (0-200)")),
            };
            if (newResults.Result > 200)
            {
                Console.WriteLine("Value cannot be higher than 200");
            }
            else
            {
                await _courseWebApiClient.AddHomeworkResult(newResults);
            }

        }

        private async void AddPresence()
        {
            List<Student> students = await _studentWebApiClient.GetAllStudents();
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.Id}. {student.Name} {student.Surname} - {student.Email}");
                StudentPresence presence = new StudentPresence();
                presence.StudentId = student.Id;
                presence.CourseId = _selectedCourse.Id;
                presence.LessonDate = _cliHelper.GetDateFromUser("Lesson date(dd-mm-yyyy): ");
                switch (_cliHelper.GetIntFromUser("Choose type of presence: \n1.Present \n2.Absent \n3.Justified"))
                {
                    case 1:
                        presence.Presence = Presence.Present;
                        break;
                    case 2:
                        presence.Presence = Presence.Absent;
                        break;
                    case 3:
                        presence.Presence = Presence.Justified;
                        break;
                    default:
                        Console.WriteLine("Wrong option");
                        break;
                }
                await _courseWebApiClient.AddPresence(presence);
                Console.Clear();
            }
        }

        private void ShowSelectedCourse()
        {
            Console.WriteLine($"Course name:  {_selectedCourse.Name} \n Trainer:  {_selectedCourse.Trainer.Name} {_selectedCourse.Trainer.Surname} \n Begin date: {_selectedCourse.BeginDate} \n");
        }

        private async void GenerateCourseResults()
        {
            List<Student> students = await _studentWebApiClient.GetAllStudentsInCourse(_selectedCourse.Id);
            List<StudentPresence> studentPresences = await _courseWebApiClient.GetCourseStudentPresence(_selectedCourse.Id);
            List<HomeworkResults> homeworkResults = await _courseWebApiClient.GetCourseHomeworkResults(_selectedCourse.Id);
            List<TestResults> testResults = await _courseWebApiClient.GetCourseTestResults(_selectedCourse.Id);

            var studentJustifiedPresence = studentPresences.GroupBy(x => x.StudentId, x => x.Presence).ToDictionary(g => g.Key, g => (float)(g.Where(x => x.Equals(Presence.Justified)).ToList().Count()/g.Where(x => x.Equals(Presence.Absent) || x.Equals(Presence.Justified)).ToList().Count()));
            
            var studentPresence = studentPresences.GroupBy(x => x.StudentId, x => x.Presence).ToDictionary(g => g.Key, g => (float)(g.Where(x => x.Equals(Presence.Present) || x.Equals(Presence.Justified)).ToList().Count()/g.ToList().Count()));
            
            var studentHomework = homeworkResults.GroupBy(x => x.StudentId, x => x.Result).ToDictionary(g => g.Key, g => g.ToList().Sum() / (g.ToList().Count() * 200));
            
            var studentTest = testResults.GroupBy(x => x.StudentId, x => x.Result).ToDictionary(g => g.Key, g => g.ToList().Sum()/(g.ToList().Count()*100));

            List<StudentResult> studentResults = new List<StudentResult>();
            foreach(var student in students)
            {
                studentResults.Add(new StudentResult()
                {
                    Student = student,
                    CourseId = _selectedCourse.Id,
                    StudentPresencePercentage = studentPresence[student.Id],
                    StudentJustifiedAbsencePercentage = studentJustifiedPresence[student.Id],
                    StudentHomeworkPercentage = studentHomework[student.Id],
                    StudentTestPercentage = studentTest[student.Id]
                });
            }

            CourseResults courseResults = new CourseResults()
            {
                Course = _selectedCourse,
                StudentResults = studentResults
            };

            _courseWebApiClient.AddCourseResults(courseResults);
        }

        public async void ShowCourseResults()
        {
            CourseResults courseResults = await _courseWebApiClient.GetCourseResults(_selectedCourse.Id);

        }
    }
}