using Influence.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Influence.Data
{
    public interface IUserRepository
    {
        void Add(User entity);
        void Delete(User entity);
        Task<bool> SaveChangesAsync();

        //User
        Task<List<User>> GetAllUsersAsync(bool includPosts = false);
        Task<User> GetUserAsync(int id);

        void UpdateUser(User user);
        //Task<User[]> GetAllUsersByEventDate(DateTime dateTime, bool includeTalks = false);
        Task<User> GetUserByName(string username);

    }
}
