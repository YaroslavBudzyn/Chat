using ChatTest.User;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

namespace ChatTest.Interfaces
{
    public interface IUserService
    {
        Task<ObjectResult> GetAll();
        Task<ObjectResult> GetById(int id);
        Task<ObjectResult> Create(UserCreateModel model);
        Task<ObjectResult> Update(UserUpdateModel model);
        Task<ObjectResult> SearchWithPaging(UserSearchModel model);
    }
}
