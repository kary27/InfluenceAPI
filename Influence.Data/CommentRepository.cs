using Influence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly InfluenceContext _context;
        private readonly ILogger<PostRepository> _logger;
        public CommentRepository(InfluenceContext context, ILogger<PostRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(Post entity)
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete(Post entity)
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

        public async Task<List<Comment>> GetAllCommentssAsync(bool includComments = false)
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
