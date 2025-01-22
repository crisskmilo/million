namespace Million.Infra.Data.Repositories.Transversal
{
    using System.Linq;
    using Million.Domain.Entities.Model.Transversal;
    using Million.Domain.Interfaces.Repositories.Transversal;

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
            var query = "SELECT * FROM dbo.[User] WHERE Email = @userName";
            return base.GetQueryData(query, new { userName = userName })?.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(int? userId)
        {
            var query = "SELECT * FROM dbo.[User] WHERE Id = @userId";
            return base.GetQueryData(query, new { userId = userId })?.FirstOrDefault();
        }
    }
}
