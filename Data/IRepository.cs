namespace DotnetAPI.Data{
    public interface IRepository<T>{
        public bool SaveChanges();
        public void AddEntity(T entityToAdd);
        public void RemoveEntity(T entityToRemove);
        public IEnumerable<T> GetAll();
        public T GetSingle(int id);
    }
}