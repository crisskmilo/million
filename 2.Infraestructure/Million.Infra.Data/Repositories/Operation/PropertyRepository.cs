using Dapper;
using ISO8583;
using Microsoft.EntityFrameworkCore;
using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Infra.Data.Repositories.Transversal;
using Million.Infra.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Million.Infra.Data.Repositories.Operation
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        AppDbContext context;

        public PropertyRepository(IDbFactory dbFactory,
             AppDbContext context) : base(dbFactory)
        {
            this.context = context;
        }

        public async Task<List<Property>> Search(SearchDto searchDto) {
           var properties =  this.context.Properties
                .AsNoTracking()
                .Include(x => x.PropertyImages)
                .AsQueryable();
            this.AddFilterWhereClause(properties, searchDto);
            return await properties.ToListAsync();
        }

        public IQueryable<Property> AddFilterWhereClause(IQueryable<Property> properties, SearchDto searchDto) {
            Type myType = properties.GetType();
            var props = this.GetObjectFields(myType);
            foreach (var prop in props)
            {
                if (searchDto.Filters.ContainsKey(prop.Name))
                {
                   properties = properties.ContainsByField(prop.Name, searchDto.Filters[prop.Name]);                    
                }
            }
            return properties;            
        }
    }

}