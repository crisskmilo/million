using Dapper;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Infra.Data.Repositories.Transversal;

namespace Million.Infra.Data.Repositories.Operation
{
    public class PropertyTraceRepository : BaseRepository<PropertyTrace>, IPropertyTraceRepository
    {
        public PropertyTraceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}