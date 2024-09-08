using Microsoft.EntityFrameworkCore;
using TaskITI.Data;
using TaskITI.Models;

namespace TaskITI.Repositories
{
    public class EmployeeRepository:IRepository<Employee>
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Employee temp)
        {
            context.employees.Add(temp);
            context.SaveChanges();
        }

        public void Delete(Employee employee)
        {

            context.employees.Remove(employee);
            context.SaveChanges();

        }

        public void DeleteAll()
        {
            context.employees.ExecuteDelete();
            context.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            return context.employees.ToList();
        }

        public Employee GetById(int id)
        {
            var result = context.employees.Find(id);
            return result;

        }

        public void Update(Employee temp)
        {
            context.employees.Update(temp);
            context.SaveChanges();
        }

    }
}
