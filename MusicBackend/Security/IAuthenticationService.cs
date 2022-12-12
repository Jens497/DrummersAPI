using MusicBackend.Models;
using System.Security.Claims;

namespace MusicBackend.Security
{
    public interface IAuthenticationService
    {
        Task<(UserRequest, string)> Authentication(string username, string password);
        string GetNameFromToken(ClaimsPrincipal claims);
    }
}
