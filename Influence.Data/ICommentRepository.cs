using Influence.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Influence.Data
{
    public interface ICommentRepository
    {
        void Add(Post entity);
        void Delete(Post entity);
        Task<bool> SaveChangesAsync();

        //Post
        Task<List<Comment>> GetAllCommentssAsync(bool includComments = false);

    }
}
