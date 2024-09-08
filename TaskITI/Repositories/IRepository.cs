namespace TaskITI.Repositories
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int id);
        public void Add(T temp);
        public void Update(T temp);
        public void Delete(T temp);
        public void DeleteAll();
    }

}
