using Microsoft.EntityFrameworkCore;
using TaskITI.Data;
using TaskITI.Models;

namespace TaskITI.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Department temp)
        {
            context.departments.Add(temp);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            context.departments.ExecuteDelete();
            context.SaveChanges();
        }
        public void Delete(Department department)
        {

            context.departments.Remove(department);
            context.SaveChanges();

        }

        public List<Department> GetAll()
        {
            return context.departments.ToList();
        }

        public Department GetById(int id)
        {
            var result = context.departments.Find(id);
            return result;

        }

        public void Update(Department temp)
        {
            context.departments.Update(temp);
            context.SaveChanges();
        }
    }
}
