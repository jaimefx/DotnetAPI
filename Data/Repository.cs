using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public class Repository<T> : IRepository<T> where T: UserIdentiy
    {
        private readonly DataContextEF<T> _entityFramework;

        public Repository(IConfiguration config)
        {
            _entityFramework = new DataContextEF<T>(config);
        }

        public bool SaveChanges(){
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity(T entityToAdd){
            if(entityToAdd != null)
                _entityFramework.Add(entityToAdd);
        }

        public void RemoveEntity(T entityToRemove){
            if(entityToRemove != null)
                _entityFramework.Remove(entityToRemove);
        }

        public IEnumerable<T> GetAll()
        {
            return _entityFramework.Context.ToList();
        }

        public T GetSingle(int id){
            T? entity = _entityFramework.Context.Find(id);

            if (entity != null)
            {
                return entity;
            }

            throw new Exception("Failed to get user");
        }
    }
}


