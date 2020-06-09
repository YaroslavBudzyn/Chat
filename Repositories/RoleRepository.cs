using ChatTest.Role;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatTest.Repositories
{
    public class RoleRepository : BaseRepository<Entities.Role>
    {
        private readonly ChatTestDbContext _dbContext;

        public RoleRepository(ChatTestDbContext contex) : base(contex)
        {
            _dbContext = contex;
        }

        public IEnumerable<Entities.Role> GetAll()
        {
            return _dbContext.Roles
                .OrderBy(r => r.Id)
                .ToList();
        }

        public int Count()
        {
            return _dbContext.Roles.Count();
        }

        public IEnumerable<Entities.Role> Search(RoleSearchModel model)
        {
            var query = from r in _dbContext.Roles
                        select r;

            if (model.Id != null)
            {
                query = query.Where(u => u.Id.Equals(model.Id));
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(u => u.Name.Contains(model.Name));
            }

            return query;
        }
    }
}
