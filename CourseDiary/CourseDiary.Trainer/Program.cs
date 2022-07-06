namespace CourseDiary.Trainer
{
    internal class Program
    {
        private LoginHelper _loginHelper;
        private Trainer _trainer;

        public Program()
        {
            _loginHelper = new LoginHelper();
        }
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
           
        }
    }
}
