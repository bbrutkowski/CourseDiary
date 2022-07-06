using System;

namespace CourseDiary.AdminClient
{
    internal class Program
    {
        private static readonly AdminClientActionHandler _adminClientActionHandler = new AdminClientActionHandler();
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            Console.WriteLine("Hello! What do you want to do? AddTrainer/");
            string action = Console.ReadLine();

            switch (action)
            {
                case "AddTrainer":
                    _adminClientActionHandler.AddTrainer();
                    break;
                case "AddStudent":
                    _adminClientActionHandler.AddStudent();
                    break;
                case "AddCourse":
                    _adminClientActionHandler.AddCourse();
                    break;
                default:
                    Console.WriteLine("unsupported option");
                    break;
            }
        }
    }
}
