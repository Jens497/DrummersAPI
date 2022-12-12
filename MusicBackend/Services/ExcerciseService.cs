using Microsoft.EntityFrameworkCore;
using MusicBackend.Data;
using MusicBackend.Dtos;
using MusicBackend.Models;

namespace MusicBackend.Services
{
    public class ExcerciseService : IExcerciseService
    {

        private readonly MusicDbContext _dbContext;

        public ExcerciseService(MusicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<tblExcercise>> GetExcercisesFromDb(string username)
        {
            var excercises = await _dbContext.Excercise.Where(x => x.Username == username).ToListAsync();
            return excercises;
        }

        public async Task CreateExcercise(tblExcercise excercise)
        {
            _dbContext.Excercise.Add(excercise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteExcercise(int excerciseId)
        {
            _dbContext.Remove(_dbContext.Excercise.Single(entry =>
            entry.Id == excerciseId));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<tblExcercise> GetSingleExcercise(int id)
        {
            var excercise = await _dbContext.Excercise.SingleAsync(entry => entry.Id == id);
            return excercise;
        }
    }
}
