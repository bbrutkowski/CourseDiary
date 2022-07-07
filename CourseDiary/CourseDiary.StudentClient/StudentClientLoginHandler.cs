using CourseDiary.StudentClient.Clients;
using CourseDiary.StudentClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.StudentClient
{
    public class StudentClientLoginHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly StudentViewWebApiClient _studentViewWebApiClient;
        private readonly StudentClientActionHandler _studentClientActionHandler;

        public StudentClientLoginHandler()
        {
            _cliHelper = new CliHelper();
            _studentViewWebApiClient = new StudentViewWebApiClient();
        }
        public string LoginLoop()
        {
            bool exit = false;
            string student = null;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("[Login] Choose action [Login, Exit]");
                switch (operation)
                {
                    case "Login":
                        student = LoginUser();
                        if (student != null)
                        {
                            _studentClientActionHandler.ProgramLoop(student);
                            break;
                        }
                        break;
                    case "Exit":
                        exit = true;
                        break;
                    default:
                        break;
                }
            }

            return student;
        }

        private string LoginUser()
        {
            string email = _cliHelper.GetStringFromUser("Add email");
            string password = _cliHelper.GetStringFromUser("Add pasword");

            bool correctCredentials = _studentViewWebApiClient.CheckStudentCredentialsAsync(email, password).Result;

            if (correctCredentials)
            {
                Console.WriteLine($"Logon successful. Hello {email}");
            }
            else
            {
                Console.WriteLine($"Login unsuccesful. Try again...");
                return null;
            }

            return email;
        }
    }
}
