using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClass.DTO;
using WebApiClass.IServices;
using WebApiClass.Model;

namespace WebApiClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent studentService;
        private readonly ILogger<StudentController> logger;

        public StudentController(IStudent studentService, ILogger<StudentController> logger)
        {
            this.studentService = studentService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("CreateStudents")]

        public IActionResult CreateStudent(StudentDTO student)
        {
            studentService.CreateStudent(student);
            return Ok("Student Successfully Created");
        }

        [HttpGet("GetAllStudents")]
       
        public IActionResult GetAllStudents()
        {
            var students = studentService.GetAllStudents();
            logger.LogInformation($"This is the list of all sudents: {students}");
            return Ok(students);
        }

        [HttpGet("GetStudentByName")]
        public IActionResult GetStudentWithName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("No Name in the input field");
            }
            if (name != null)
            {
                var student = studentService.GetStudentByName(name);
                return Ok(student);
            }
            return Ok();
        }

        [HttpPost("UpdateStudent")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            if (id != student.Id) return BadRequest();
            studentService.UpdateStudentId(student);
            return Ok("Student successfully Updated");
        }

    }
}
