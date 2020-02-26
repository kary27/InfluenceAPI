using Influence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Influence.Data
{
    public class PostRepository : IPostRepository
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
            _logger.LogInformation($"Adding a new post to the context.");
            _context.Add(entity);
        }

        public void Delete(Post entity)
        {
            _logger.LogInformation("Removing a post from the context.");
            _context.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            _logger.LogInformation("Return all posts");
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<List<Post>> GetAllPostsForUser(int userId)
        {
            return await _context.Posts.Where(p => p.UserId == userId)
                .Include(p=>p.Comments).ToListAsync();
        }

        public async Task<List<Post>> GetAllPostsWithComments()
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Likes).ToListAsync();
        }

        public void Update(Post entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
