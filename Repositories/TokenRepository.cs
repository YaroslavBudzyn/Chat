using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.Repositories
{
    public class TokenRepository : BaseRepository<Entities.Token>
    {
        private readonly ChatTestDbContext _dbContext;

        public TokenRepository(ChatTestDbContext contex) : base(contex)
        {
            _dbContext = contex;
        }

        public IEnumerable<Entities.Token> GetAll()
        {
            return _dbContext.Tokens
                .Include(t => t.User)
                .OrderBy(t => t.Id)
                .ToList();
        }
        public Entities.Token GetByUserId(int id)
        {
            return _dbContext.Tokens.FirstOrDefault(u => u.UserId.Equals(id));
        }

        public IEnumerable<Entities.Token> GetAllByUserId(int id)
        {
            return _dbContext.Tokens.Where(u => u.UserId.Equals(id));
        }

        public Entities.User GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(e => e.Email.Equals(email));
        }

        public void DeleteByToken(string code)
        {
            Entities.Token token = _dbContext.Tokens.FirstOrDefault(t => t.Code.Equals(code));

            if (token != null)
            {
                _dbContext.Tokens.Remove(token);
            }
        }

        public Entities.Token GetByToken(string token)
        {
            if (token.Contains("Bearer"))
            {
                var code = token.Split("Bearer").Last().Trim();
                return _dbContext.Tokens.Include(u => u.User).FirstOrDefault(t => t.Code.Equals(code));
            }

            if (token.Contains("bearer"))
            {
                var code = token.Split("bearer").Last().Trim();
                return _dbContext.Tokens.Include(u => u.User).FirstOrDefault(t => t.Code.Equals(code));
            }

            return null;
        }

        public Entities.Token GetByRefreshToken(string code)
        {
            return _dbContext.Tokens.FirstOrDefault(t => t.RefreshToken.Equals(code));
        }
    }
}
