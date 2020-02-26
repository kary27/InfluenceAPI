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

        public void Add(Comment entity)
        {
            _logger.LogInformation($"Adding a new comment to the context.");
            _context.Add(entity);
        }

        public void Delete(Comment entity)
        {
            _logger.LogInformation($"Removing a comment from the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<Comment>> GetAllCommentssAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public void Update(Comment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Comment> GetComment(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
    }
}
