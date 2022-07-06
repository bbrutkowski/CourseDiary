using CourseDiary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.Domain.Interfaces
{
    public interface ITrainerRepository
    {
        Task<bool> AddTrainer(Trainer trainer);
        Task<Trainer> GetTrainerByEmail(string trainerName);
        Task<List<Trainer>> GetAllTrainers();
        Task<Trainer> GetTrainer(int id);
    }
}
