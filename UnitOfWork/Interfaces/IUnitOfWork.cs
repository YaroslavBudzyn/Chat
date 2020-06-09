using ChatTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserRepository Users { get; }
        TokenRepository Tokens { get; }
        RoleRepository Roles { get; }
        UserGroupRepository UserGroups { get; } 

        bool Save();
    }
}
