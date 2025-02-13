using WebApiClass.Model;

namespace WebApiClass.IServices
{
    public interface IStudent
    {
        //Return type and the functionName add parameters if required
        void CreateStudent(Student student);
        Student GetStudentById(string id);
        Student GetStudentByName(string name);
        void DeleteStudentById(Student student);
        void UpdateStudentId(string id);
        ICollection<Student> GetAllStudents();
    }
}
