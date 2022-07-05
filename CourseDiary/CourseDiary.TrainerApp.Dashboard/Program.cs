using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.TrainerApp.Dashboard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private readonly LoginHandler _loginHandler;
        private readonly TrainerActionsHandler _trainerActionsHandler;
        public Program()
        {
            _loginHandler = new LoginHandler();
            _trainerActionsHandler = new TrainerActionsHandler();
        }

        private void Run()
        {
            string loggedUser = _loginHandler.LoginLoop();

            if (!string.IsNullOrEmpty(loggedUser))
            {
                _trainerActionsHandler.ProgramLoop(loggedUser);
            }
        }
    }
}
