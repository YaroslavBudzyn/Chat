

namespace ChatTest.Interfaces
{
    public interface IJwtHandler
    {
        Entities.Token Generate(Entities.User user, string role);
    }
}
