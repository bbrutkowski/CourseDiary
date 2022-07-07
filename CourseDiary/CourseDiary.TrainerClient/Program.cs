using CourseDiary.TrainerClient.Models;

namespace CourseDiary.TrainerClient
{
    internal class Program
    {
        private readonly TrainerClientLoginHandler _trainerClientLoginHandler;
        private readonly TrainerClientActionHandler _trainerClientActionHandler;
        private Trainer _loggedTrainer;
        private string _loggedUser = string.Empty;
        public Program()
        {
            _trainerClientLoginHandler = new TrainerClientLoginHandler();
            _trainerClientActionHandler = new TrainerClientActionHandler();
            _loggedTrainer = new Trainer();
        }
        static void Main(string[] args)
        {
            new Program().Run();
        }
        private void Run()
        {
            _loggedTrainer = _trainerClientLoginHandler.LoginLoop();

            if (_loggedTrainer != null)
            {
                _trainerClientActionHandler.ProgramLoop(_loggedTrainer.Email);
            }
        }
    }
}
