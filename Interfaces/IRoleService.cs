using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

namespace ChatTest.Interfaces
{
    public interface IRoleService
    {
        Task<ObjectResult> GetAll();
        Task<ObjectResult> GetById(int id);
        Task<ObjectResult> Update(UpdateRoleIncomeModel model);
        Task<ObjectResult> SearchWithPaging(RoleSearchModel model);
    }
}
