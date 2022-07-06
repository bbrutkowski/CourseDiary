using CourseDiary.Domain.Models;
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
        private Trainer _loggedTrainer = null;
        public Program()
        {
            _loginHandler = new LoginHandler();
            _trainerActionsHandler = new TrainerActionsHandler();
            _loggedTrainer = new Trainer();
        }

        private void Run()
        {
            _loggedTrainer = _loginHandler.LoginLoop();

            if (_loggedTrainer != null)
            {
                _trainerActionsHandler.ProgramLoop(_loggedTrainer);
            }
        }
    }
}
