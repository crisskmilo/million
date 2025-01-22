using Million.Domain.Entities.Model.Transversal;

namespace Million.Domain.Interfaces.Repositories.Transversal
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserAuth(string userName);
        User GetUserById(int? id);
    }
}
