namespace Million.Domain.Interfaces.Repositories.Transversal
{
    public interface IBaseRepository<T>: IQueryRepository<T> where T : class
    {
        public long Insert(T entity);
        public void Delete(T entity);
        public void Update(T entity);
    }
}
