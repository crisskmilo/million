namespace Million.Domain.Interfaces.Repositories.Transversal
{
    using System.Data;

    public interface IDbFactory
    {
        /// <summary>
        /// The get connection.
        /// </summary>
        /// <returns>
        /// The <see cref="IDbConnection"/>.
        /// </returns>
        IDbConnection GetConnection();
    }
}
