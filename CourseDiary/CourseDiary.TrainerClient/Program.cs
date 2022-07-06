namespace CourseDiary.TrainerClient
{
    internal class Program
    {
        private readonly TrainerClientLoginHandler _trainerClientLoginHandler;
        private readonly TrainerClientActionHandler _trainerClientActionHandler;
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public Program()
        {
            _trainerClientLoginHandler = new TrainerClientLoginHandler();
            _trainerClientActionHandler = new TrainerClientActionHandler();
        }

        private void Run()
        {
            string loggedUser = _trainerClientLoginHandler.LoginLoop();

            if (!string.IsNullOrEmpty(loggedUser))
            {
                bool exit;
                do
                {
                    exit = _trainerClientActionHandler.ProgramLoop(loggedUser);
                } while (!exit);
            }
        }
    }
}
