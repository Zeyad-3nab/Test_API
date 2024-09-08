using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskITI.DT0;
using TaskITI.Models;
using TaskITI.Repositories;

namespace TaskITI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> employeeRepository;

        public EmployeeController(IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var result = employeeRepository.GetAll();
            return Ok(result);
        }



        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = employeeRepository.GetById(id);
            if (result != null)
            {
                employeeRepository.Delete(result);
                return Ok(result);
            }
            return BadRequest("Not Found");
        }



        [HttpDelete]
        public IActionResult DeleteALL()
        {
            employeeRepository.DeleteAll();
            return Ok();
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = employeeRepository.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        [HttpPost]
        public IActionResult Add([FromBody] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Address = employeeDTO.Address,
                    Salary = employeeDTO.Salary,
                    Name = employeeDTO.Name,
                    DepartmentId = employeeDTO.DepartmentId

                };
                employeeRepository.Add(employee);
                return Ok(employee);
            }
            return BadRequest("Error in model state");
        }



        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Id = id,
                    Address = employeeDTO.Address,
                    Salary = employeeDTO.Salary,
                    Name = employeeDTO.Name,
                    DepartmentId = employeeDTO.DepartmentId

                };
                employeeRepository.Update(employee);
                return Ok(employee);
            }
            return BadRequest("Error in model state");
        }


    }
}
