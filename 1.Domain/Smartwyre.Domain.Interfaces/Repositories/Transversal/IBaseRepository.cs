namespace Smartwyre.Domain.Interfaces.Repositories.Transversal
{
    using Smartwyre.Domain.Entities.Model.Transversal;

    public interface IBaseRepository<T>: IQueryRepository<T> where T : BaseEntity
    {
       public long Insert(T entity);

       public void Delete(T entity);

       public void Update(T entity);
    }
}
