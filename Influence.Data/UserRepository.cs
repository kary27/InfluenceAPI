using Influence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly InfluenceContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(InfluenceContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(User entity)
        {
            _logger.LogInformation($"Adding a new user to the context.");
            _context.Add(entity);
        }

        public void Delete(User entity)
        {
            _logger.LogInformation($"Removing an user to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<User>> GetAllUsersAsync(bool includPosts = false)
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<User> GetUserByName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}