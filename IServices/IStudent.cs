using WebApiClass.DTO;
using WebApiClass.Model;

namespace WebApiClass.IServices
{
    public interface IStudent
    {
        //Return type and the functionName add parameters if required
        void CreateStudent(StudentDTO studentDto);
        Student GetStudentById(string id);
        Student GetStudentByName(string name);
        void DeleteStudentById(Student student);
        void UpdateStudentId(Student student);
        ICollection<Student> GetAllStudents();
    }
}
