using Microsoft.Owin.Hosting;
using System;

namespace CourseDiary.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = @"http://localhost:9000/";

            using (WebApp.Start<StartUp>(baseAddress))
            {
                Console.WriteLine("API Started");
                Console.ReadKey();
            }
        }
    }
}
