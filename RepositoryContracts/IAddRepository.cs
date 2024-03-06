namespace RepositoryContracts
{
    public interface IAddRepository<T> where T : class
    {
        public Task<bool> AddAsync(T entity);
    }
}
