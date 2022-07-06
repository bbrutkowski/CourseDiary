using CourseDiary.TrainerClient.Clients;
using System;

namespace CourseDiary.TrainerClient
{
    internal class TrainerClientActionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly TrainerWebApiClient _trainerWebApiClient;
        private readonly CourseWebApiClient _courseWebApiClient;
        public bool ProgramLoop(string loggedUser)
        {
            bool exit = false;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("");

                switch (operation)
                {
                    case "1":
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
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }

            return exit;
        }
    }
}
