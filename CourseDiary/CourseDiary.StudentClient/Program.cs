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
        private string _loggedStudent = null;
        public Program()
        {
            _studentClientLoginHandler = new StudentClientLoginHandler();
            _studentClientActionHandler = new StudentClientActionHandler();
        }
        static void Main(string[] args)
        {
            new Program().Run();
        }
        private void Run()
        {
            _loggedStudent = _studentClientLoginHandler.LoginLoop();

            if (_loggedStudent != null)
            {
                _studentClientActionHandler.ProgramLoop(_loggedStudent);
            }
        }
    }
}
