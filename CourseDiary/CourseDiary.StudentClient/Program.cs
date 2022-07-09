using CourseDiary.StudentClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.StudentClient
{
    internal class Program
    {
        private readonly StudentClientLoginHandler _studentClientLoginHandler;
        private readonly StudentClientActionHandler _studentClientActionHandler;
        public Program()
        {
            _studentClientLoginHandler = new StudentClientLoginHandler();
            _studentClientActionHandler = new StudentClientActionHandler();
        }
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            string loggedStudent = _studentClientLoginHandler.LoginLoop();

            if (!string.IsNullOrEmpty(loggedStudent))
            {
                _studentClientActionHandler.ProgramLoop(loggedStudent);
            }
        }
    }
}
