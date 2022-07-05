using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.TrainerApp.Dashboard
{
    internal class TrainerActionsHandler
    {
        private readonly CliHelper _cliHelper;
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
