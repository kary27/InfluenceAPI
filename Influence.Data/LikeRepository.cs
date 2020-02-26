using Influence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly InfluenceContext _context;

        public LikeRepository(InfluenceContext context)
        {
            _context = context;
        }
        public void Add(Like entity)
        {
            _context.Likes.Add(entity);
        }

        public void Delete(Like entity)
        {
            _context.Likes.Remove(entity);
        }

        public async Task<List<Like>> GetAllLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        public async Task<Like> GetLikeById(int id)
        {
            return await _context.Likes.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
