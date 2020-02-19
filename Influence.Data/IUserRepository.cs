using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Influence.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //User
        Task<User[]> GetAllUsersAsync(bool includPosts = false);
        Task<User> GetUserAsync(string moniker, bool includeTalks = false);
        //Task<User[]> GetAllUsersByEventDate(DateTime dateTime, bool includeTalks = false);

    }
}
