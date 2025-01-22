namespace Million.Domain.Interfaces.Services
{
    using Million.Domain.Entities.Model.Transversal;
    using Million.Domain.Interfaces.Services.Transversal;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IUserService" />
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="userName"></param>
       /// <param name="password"></param>
       /// <returns></returns>
        User SignIn(string userName, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        User GetUserAuth(string userName);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="userName"></param>
       /// <returns></returns>
        string GetToken(string userName, int? id);
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserById(int? userId);

        IEnumerable<User> GetUsers();

    }
}
