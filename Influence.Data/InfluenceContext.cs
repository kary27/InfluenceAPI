using Influence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Influence.Data
{
    public class InfluenceContext: DbContext
    {
        public InfluenceContext(DbContextOptions contextOptions)
            :base(contextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
