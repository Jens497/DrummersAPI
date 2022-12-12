using MusicBackend.Data;
using MusicBackend.Models;
using System.Security.Claims;

namespace MusicBackend.Services
{
    public interface IUserService
    {
        Task UpdateSelectUser(tblUser user);
        Task<bool> checkIfUsernameTaken(string username);
        Task RegisterUser(tblUser user);
    }
}
