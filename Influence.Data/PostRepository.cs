using Influence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Influence.Data
{
    public class PostRepository
    {
        private readonly InfluenceContext _context;
        private readonly ILogger<UserRepository> _logger;

        public PostRepository(InfluenceContext context, ILogger<UserRepository> logger)
        {        
            _context = context;
            _logger = logger;
        }

        public void Add(Post entity)
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete(User entity)
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<User>> GetAllPostAsync(bool includeComment = false)
        {
            return await _context.Users.ToListAsync();
        }
    }
}
