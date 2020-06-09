using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    public class UserRepositoryTest
    {
        [Fact]
        public void GetUserByEmailReturnsResult()
        {
            using (var context = new ChatTestDbContext(CreateNewContextOptions()))
            {
                context.AddRange(GetUsers());
                context.SaveChanges();

                var repository = new UserRepository(context);
                var result = repository.GetByEmail("admin@admin.com");

                Assert.NotNull(result);
            }
        }

        [Fact]
        public void CreateUserSuccess()
        {
            using (var context = new ChatTestDbContext(CreateNewContextOptions()))
            {
                context.AddRange(GetUsers().First());
                context.SaveChanges();

                var result = context.Users.Any();

                Assert.True(result);
            }
        }

        [Fact]
        public void DeleteUserSuccess()
        {
            using (var context = new ChatTestDbContext(CreateNewContextOptions()))
            {
                var repository = new UserRepository(context);
                repository.Create(GetUsers().First());
                context.SaveChanges();
                repository.Delete(1);
                context.SaveChanges();
                var result = context.Users.Any();
                Assert.False(result);
            }
        }

        [Fact]
        public void GetAllReturnsCorrectResult()
        {
            using (var context = new ChatTestDbContext(CreateNewContextOptions()))
            {
                context.AddRange(GetUsers());
                context.SaveChanges();

                var repository = new UserRepository(context);
                var result = repository.GetAll();

                Assert.Empty(result);
            }
        }

        [Fact]
        public void CountUserReturnsCorrectResult()
        {
            using (var context = new ChatTestDbContext(CreateNewContextOptions()))
            {
                context.AddRange(GetUsers());
                context.SaveChanges();

                var repository = new UserRepository(context);
                var result = repository.Count();

                Assert.Equal(2, result);
            }
        }

        private static DbContextOptions<ChatTestDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ChatTestDbContext>();
            builder.UseInMemoryDatabase("ChatTestDbContext")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private static List<User> GetUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    //password admin123
                    Name = "admin",
                    RoleId = 1,
                    Email = "admin@admin.com",
                    Phone = "95462362",
                    Password = "threthretherthrg151454rg"
                },
                new User
                {
                    //password user123
                    Name = "user",
                    RoleId = 2,
                    Email = "user@user.com",
                    Phone = "646546626",
                    Password = "rgergrtyghtr156564retgretre"
                }
            };
            return users;
        }
    }
}
