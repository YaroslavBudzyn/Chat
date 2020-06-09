using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ChatTestDbContext _dbContext;

        protected DbSet<T> DbSet;

        public BaseRepository(ChatTestDbContext contex)
        {
            _dbContext = contex;
            DbSet = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }
    }
}
