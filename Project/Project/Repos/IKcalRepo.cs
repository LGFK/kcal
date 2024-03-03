namespace Project.Repos
{
    public interface IKcalRepo<T>
    {
        public Task<T> GetEntity(int id);
        public Task<List<T>> GetEntitiesList();
        public Task DeleteEntity(int id);

        public Task UpdateEntity(T entity);

        public Task AddEntity(T entity);

        public Task AddEntities(List<T> entities);
    }
}
