using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiClass.Data;
using WebApiClass.DTO;
using WebApiClass.IServices;
using WebApiClass.Model;

namespace WebApiClass.Services
{
    public class StudentService : IStudent
    {
        private readonly StudentDbContext studentDbContext;
        private readonly IMapper mapper;


        public StudentService(StudentDbContext studentDbContext, IMapper mapper)
        {
            this.studentDbContext = studentDbContext;
            this.mapper = mapper;
        }

        public void CreateStudent(StudentDTO studentDto)
        {
            var student = mapper.Map<Student>(studentDto);
            studentDbContext.Students.Add(student);
            studentDbContext.SaveChanges();
        }

        public StudentDTO GetStudentDTO(Student student)
        {
            return mapper.Map<StudentDTO>(student);
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
            return studentDbContext.Students.FirstOrDefault(s => s.FirstName == name || s.LastName == name);
        }

        public void UpdateStudentId(Student student)
        {           
            if (student != null)
            {
                studentDbContext.Students.Update(student);
                studentDbContext.SaveChanges();
            }
        }
    }
}
