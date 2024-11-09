namespace Smartwyre.Domain.Interfaces.Repositories.Transversal
{
    using Smartwyre.Domain.Entities;
    using Smartwyre.Domain.Entities.Model.Authentication;
    using Smartwyre.Domain.Entities.Model.Transversal;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;
    using System.Collections.Generic;

    public interface IUserRepository : IBaseRepository<User>
    {
        // IEnumerable<User> ObtenerUsuariosFiltro(UsuarioFiltro filtro);

        //  IEnumerable<User> GetUsers(int id = 0);

        User GetUserAuth(string userName);        

        // string ValidarExistencia(User user);

         User GetUserById(int? id);

        //bool ExistsUserByRolValidation(int rolId);

        // bool RolAsignedUserValidation(Rol rol);

        // IEnumerable<User> GetByRol(string rolId);
    }
}
