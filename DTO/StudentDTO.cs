using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebApiClass.Model;

namespace WebApiClass.DTO
{
    public class StudentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Stack { get; set; }
    
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
