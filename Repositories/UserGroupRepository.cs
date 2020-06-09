using ChatTest.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.Repositories
{
    public class UserGroupRepository : BaseRepository<Entities.UserGroup>
    {
        private readonly ChatTestDbContext _dbContext;

        public UserGroupRepository(ChatTestDbContext contex) : base(contex)
        {
            _dbContext = contex;
        }

        public new Entities.UserGroup Get(int id)
        {
            return _dbContext.UserGroup
                .Include(r => r.GroupId)
                .SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<Entities.UserGroup> GetAll()
        {
            return _dbContext.UserGroup
                .Include(r => r.GroupId)
                .OrderBy(u => u.Id)
                .ToList();
        }

        public int Count()
        {
            return _dbContext.UserGroup.Count();
        }

        public Entities.User GetByEmail(string userName)
        {
            return _dbContext.UserGroup.FirstOrDefault(e => e.UserName.Equals(userName));
        }

        public IEnumerable<Entities.User> Search(UserGroupSearchModel model)
        {
            var query = from u in _dbContext.UserGroup
                    .Include(r => r.GroupId)
                        select u;


            if (model.Id != null)
            {
                query = query.Where(u => u.Id.Equals(model.Id));
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                query = query.Where(u => u.UserName.ToLower().Contains(model.UserName.ToLower()));
            }

            return query;
        }
    }
}
