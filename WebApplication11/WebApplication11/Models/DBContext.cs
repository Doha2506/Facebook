using Microsoft.EntityFrameworkCore;

namespace WebApplication11.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Newpost> newpost { get; set; }

        public DbSet<friendRequest> FriendRequests { get; set; }

        public DbSet<Comment> comment { get; set; }
        public DbSet<RequestDetail> requestdetail { get; set; }
        public DbSet<Friends> friends { get; set; }
    }
}
