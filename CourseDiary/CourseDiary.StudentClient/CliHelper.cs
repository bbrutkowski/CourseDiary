using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.StudentClient
{
    internal class CliHelper
    {
        public string GetStringFromUser(string message)
        {
            string inputFromUser;

            do
            {
                Console.Write($"{message}: \n");
                inputFromUser = Console.ReadLine();

                if (string.IsNullOrEmpty(inputFromUser))
                {
                    Console.WriteLine("You have to type something");
                }

            } while (string.IsNullOrEmpty(inputFromUser));

            return inputFromUser;
        }
        public int GetIntFromUser(string message)
        {
            int result = 0;
            bool success = false;

            while (!success)
            {
                string text = GetStringFromUser(message);
                success = int.TryParse(text, out result);

                if (!success)
                {
                    Console.WriteLine("Not a number. Try again...");
                }
            }

            return result;
        }

        public DateTime GetDateFromUser(string message)
        {
            Console.WriteLine(message);
            string line = Console.ReadLine();
            DateTime data;
            while (!DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out data))
            {
                Console.WriteLine("Invalid date, please retry");
                line = Console.ReadLine();
            }

            return data;
        }

        internal float GetRateFromUser(string message)
        {
            int result = 0;
            bool success = false;

            do
            {
                string text = GetStringFromUser(message);
                success = int.TryParse(text, out result);

                if (!success)
                {
                    Console.WriteLine("Not a number. Try again...");
                }

                if(result>10 || result < 1)
                {
                    Console.WriteLine("You have to choose rate between 1 and 10");
                }

            }while(result > 10 || result < 1);

            return result;
        }
    }
}
