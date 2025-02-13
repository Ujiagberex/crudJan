using WebApiClass.Data;
using WebApiClass.IServices;
using WebApiClass.Model;

namespace WebApiClass.Services
{
    public class StudentService : IStudent
    {
        private readonly StudentDbContext studentDbContext;

        public StudentService(StudentDbContext studentDbContext)
        {
            this.studentDbContext = studentDbContext;
        }

        public void CreateStudent(Student student)
        {
            studentDbContext.Students.Add(student);
            studentDbContext.SaveChanges();
        }

        public void DeleteStudentById(Student student)
        {
            throw new NotImplementedException();
        }

        public ICollection<Student> GetAllStudents()
        {
           return studentDbContext.Students.ToList();
        }

        public Student GetStudentById(string id)
        {
            throw new NotImplementedException();
        }

        public Student GetStudentByName(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudentId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
