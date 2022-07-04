using CourseDiary.Domain;
using CourseDiary.Domain.Models;
using CourseDiary.Infrastructure;
using System;
using System.Threading.Tasks;

namespace CourseDiary
{
    internal class Program
    {
        private TrainerService _trainerService;

        public Program()
        {
            _trainerService = new TrainerService(new TrainerRepository());
        }
        
        static void Main(string[] args)
        {
            new Program().Run();          
        }

        private void Run()
        {
            Console.WriteLine("Hello! What do you want to do?");
            string action = Console.ReadLine();

            switch (action)
            {
                case "Add trainer":
                    AddTrainer();
                        break;
                default:
                    Console.WriteLine("unsupported option");
                    break;
            }
        }

        public async void AddTrainer()
        {

            Trainer newTrainer = new Trainer
            {
                Name = Console.ReadLine(),
                Surname = Console.ReadLine(),
                Password = Console.ReadLine(),
                Email = Console.ReadLine(),
                DateOfBirth = Console.ReadLine(),
            };

            var isAdded = await _trainerService.AddTrainer(newTrainer);
            if (isAdded == true)
            {
                Console.WriteLine("Trainer added successfully");
            }
        }
    }
}
