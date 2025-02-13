using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiClass.IServices;
using WebApiClass.Model;

namespace WebApiClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent studentService;

        public StudentController(IStudent studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost]
        [Route("CreateStudents")]

        public IActionResult CreateStudent(Student student)
        {
            studentService.CreateStudent(student);
            return Ok("Student Successfully Created");
        }

        [HttpGet("GetAllStudents")]
       
        public IActionResult GetAllStudents()
        {
            var students = studentService.GetAllStudents();
            return Ok(students);
        }

    }
}
