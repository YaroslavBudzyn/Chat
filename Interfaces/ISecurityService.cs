using ChatTest.User;
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

namespace ChatTest.Interfaces
{
    public interface ISecurityService
    {
        Task<ObjectResult> Login(LoginIncomeModel model);
    }
}
