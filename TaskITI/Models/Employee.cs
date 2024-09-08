namespace TaskITI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }

        public int DepartmentId { get; set; }

        public Department department { get; set; }
    }
}
