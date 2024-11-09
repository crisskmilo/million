namespace Smartwyre.Application.Interfaces.Transversal
{
    using Smartwyre.Domain.Entities.Model.Authentication;
    using Smartwyre.Domain.Entities.Response;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUserApplication
    {
        GenericResponse<User> SignIn(string userName, string password);

        GenericResponse<User> GetUserAuth(string userName);

        GenericResponse<string> GetToken(string userName, int? id);

        GenericResponse<IEnumerable<User>> GetUsers();
    }
}
