using Million.Domain.Entities.Model.Transversal;
using Million.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Million.Application.Interfaces.Transversal
{
    public interface IUserApplication
    {
        GenericResponse<User> SignIn(string userName, string password);

        GenericResponse<User> GetUserAuth(string userName);

        GenericResponse<string> GetToken(string userName, int? id);

        GenericResponse<IEnumerable<User>> GetUsers();
    }
}
