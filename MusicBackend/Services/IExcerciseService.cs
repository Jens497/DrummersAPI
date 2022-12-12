using MusicBackend.Data;
using MusicBackend.Dtos;

namespace MusicBackend.Services
{
    public interface IExcerciseService
    {
        Task<IEnumerable<tblExcercise>> GetExcercisesFromDb(string username);
        Task CreateExcercise(tblExcercise excercise);
        Task DeleteExcercise(int excerciseId);
        Task<tblExcercise> GetSingleExcercise(int id);
    }
}
