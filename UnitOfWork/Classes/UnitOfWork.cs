using ChatTest.Context;
using ChatTest.Repositories;
using ChatTest.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.UnitOfWork.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatTestDbContext _dbContext;
        private UserRepository _userRepository;
        private RoleRepository _roleRepository;
        private TokenRepository _tokenRepository;
        private UserGroupRepository _userGroupRepository;

        // Constructor
        public UnitOfWork(ChatTestDbContext db)
        {
            _dbContext = db;
        }

        public UserRepository Users => _userRepository ?? (_userRepository = new UserRepository(_dbContext));
        public TokenRepository Tokens => _tokenRepository ?? (_tokenRepository = new TokenRepository(_dbContext));
        public RoleRepository Roles => _roleRepository ?? (_roleRepository = new RoleRepository(_dbContext));
        public UserGroupRepository UserGroups => _userGroupRepository ?? (_userGroupRepository = new UserGroupRepository(_dbContext));

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        private bool _disposed;


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
