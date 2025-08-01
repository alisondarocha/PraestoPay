namespace PraestoPay.Domain.AggregatesModel;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> GetByIdAsync<TId>(TId id);
}
