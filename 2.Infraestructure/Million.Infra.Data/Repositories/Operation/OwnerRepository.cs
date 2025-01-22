using Dapper;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Infra.Data.Repositories.Transversal;

namespace Million.Infra.Data.Repositories.Operation
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}
