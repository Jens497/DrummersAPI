using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using MusicBackend.Data;
using MusicBackend.Models;
using System.Security.Claims;

namespace MusicBackend.Services
{
    public class UserServices : IUserService
    {
        private readonly MusicDbContext _dbContext;

        public UserServices(MusicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> checkIfUsernameTaken(string username)
        {
            var user = await _dbContext.User.Where(x => x.Username == username).FirstOrDefaultAsync();
            if(user == null)
                return false;
            else
                return true;
         
        }

        public async Task RegisterUser(tblUser user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSelectUser(tblUser user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
