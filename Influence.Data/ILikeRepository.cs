using Influence.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Data
{
    public interface ILikeRepository
    {
        void Add(Like entity);
        void Delete(Like entity);
        Task<bool> SaveChangesAsync();
        Task<List<Like>> GetAllLikes();
        Task<Like> GetLikeById(int id);

    }
}
