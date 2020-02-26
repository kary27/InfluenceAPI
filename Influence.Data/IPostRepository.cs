using Influence.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Influence.Data
{
    public interface IPostRepository
    {
        void Add(Post entity);
        void Delete(Post entity);
        void Update(Post entity);
        Task<bool> SaveChangesAsync();

        //Post
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostAsync(int id);

        Task<List<Post>> GetAllPostsForUser(int userId);

        Task<List<Post>> GetAllPostsWithComments();
    }
}
