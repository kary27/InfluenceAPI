using Influence.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Data
{
    public interface ICommentRepository
    {
        void Add(Comment entity);
        void Delete(Comment entity);
        void Update(Comment entity);
        Task<bool> SaveChangesAsync();

        Task<List<Comment>> GetAllCommentssAsync();
        Task<Comment> GetComment(int id);
        

    }
}
