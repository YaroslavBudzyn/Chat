using ChatTest.Entities;
using System.Data.Entity;

namespace ChatTest.Context
{
    public class ChatTestDbContext : DbContext
    {
        public ChatTestDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<ChatTestDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
