using ChatTest.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatTest.Repositories
{
    public class UserRepository : BaseRepository<Entities.User>
    {
        private readonly ChatTestDbContext _dbContext;

        public UserRepository(ChatTestDbContext contex) : base(contex)
        {
            _dbContext = contex;
        }

        public new Entities.User Get(int id)
        {
            return _dbContext.Users
                .Include(r => r.Role)
                .SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<Entities.User> GetAll()
        {
            return _dbContext.Users
                .Include(r => r.Role)
                .OrderBy(u => u.Id)
                .ToList();
        }

        public int Count()
        {
            return _dbContext.Users.Count();
        }

        public Entities.User GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Email.Equals(email));
        }

        public IEnumerable<Entities.User> Search(UserSearchModel model)
        {
            var query = from u in _dbContext.Users
                    .Include(r => r.Role)
                        select u;

            if (!string.IsNullOrEmpty(model.Phone))
            {
                query = query.Where(u => u.Phone.ToLower().Contains(model.Phone.ToLower()));
            }

            if (model.Id != null)
            {
                query = query.Where(u => u.Id.Equals(model.Id));
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(u => u.Name.ToLower().Contains(model.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                query = query.Where(u => u.Email.ToLower().Contains(model.Email.ToLower()));
            }

            if (model.RoleId != null)
            {
                query = query.Where(u => u.RoleId.Equals(model.RoleId));
            }

            if (!string.IsNullOrEmpty(model.Role))
            {
                query = query.Where(i => i.Role.Name.ToLower().Contains(model.Role.ToLower()));
            }

            return query;
        }
    }
}
