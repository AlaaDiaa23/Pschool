using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pschool.DTO;
using Pschool.Repository;
using Pschool.Repository.Base;
using System.Diagnostics;
using System.Reflection;

namespace Pschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IBaseRepository<Student> _studentrepository;
        private readonly IStudentRepo student;
        private readonly IBaseRepository<Parent> p;

        public StudentsController(IBaseRepository<Student>studentrepository,IStudentRepo student)
        {
            _studentrepository = studentrepository;
            this.student = student;
           
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await student .GetStudentsAsync());
            }
            catch (Exception)
            {
                return BadRequest("error");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await student.GetById(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Studentdto studentdto)
        {

            var Student = new Student
            {
                FirstName = studentdto.FirstName,
                LastName = studentdto.LastName,
                Address = studentdto.Address,
                DateOfBirth = studentdto.DateOfBirth,
                Grade=studentdto.Grade,
                Year_Group = studentdto.Year_Group,
                ParentId = studentdto.ParentId
                






            };
            await _studentrepository.CreateAsync(Student);
            return Ok(Student);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Studentdto studentdto)
        {
            var student = await _studentrepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound("error");
            }
            student.FirstName = studentdto.FirstName;
            student.LastName = studentdto.LastName;
            student.Address = studentdto.Address;
            student.DateOfBirth = studentdto.DateOfBirth;
            student.Grade = studentdto.Grade;
            student.Year_Group = studentdto.Year_Group;
            student.ParentId = studentdto.ParentId;



            await _studentrepository.UpdateAsync(student.Id);
            return Ok(student);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentrepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound("error");
            }
            await _studentrepository.DeleteAsync(student.Id);
            return Ok(student);
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Student>>> Search(string name)
        {
            try
            {
                var result = await student.Search(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
       
    }



}


