namespace Smartwyre.Infra.Data.Repositories.Transversal
{
    using System.Linq;
    using Dapper;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;
    using Smartwyre.Domain.Entities.Model.Authentication;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserAuth(string userName)
        {
            var query = "SELECT * FROM [perezgomez].[usuarios] WHERE usuario = @userName";
            return base.GetQueryData(query, new { userName = userName })?.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(int? userId)
        {
            var query = "SELECT * FROM [perezgomez].[usuarios] WHERE id = @userId";
            return base.GetQueryData(query, new { userId = userId })?.FirstOrDefault();
        }
    }
}
