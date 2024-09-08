using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskITI.DT0;
using TaskITI.Models;
using TaskITI.Repositories;

namespace TaskITI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository<Department> departmentRepository;

        public DepartmentController(IRepository<Department> departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var result = departmentRepository.GetAll();
            return Ok(result);
        }



        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var result = departmentRepository.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }



        [HttpPost]
        public IActionResult Add([FromBody] DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Name = departmentDTO.Name
                };
                departmentRepository.Add(department);
                return Ok(department);
            }
            return BadRequest("Error in model state");
        }



        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Id = id,
                    Name = departmentDTO.Name
                };
                departmentRepository.Update(department);
                return Ok(department);
            }
            return BadRequest("Error in model state");
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var result = departmentRepository.GetById(id);
                if (result != null)
                {
                    departmentRepository.Delete(result);
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest("Error in model state");
        }


        [HttpDelete]
        public IActionResult DeleteALL()
        {
            departmentRepository.DeleteAll();
            return Ok();
        }
    }
}