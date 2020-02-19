using Influence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Influence.Data
{
    public class UserRepository
    {
        private readonly InfluenceContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(InfluenceContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<User[]> GetAllUsersAsync(bool includPosts = false)
        {
            _logger.LogInformation($"Getting all Camps");

            IQueryable<User> query = _context.Users.Include(u => u.UserName);

            if (includPosts)
            {
                query = query.Include(u => u.Posts);
            }

            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserAsync(string moniker, bool includeTalks = false)
        {

        }
    }
}