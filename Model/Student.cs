using System.ComponentModel.DataAnnotations;

namespace WebApiClass.Model
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [StringLength(15)]
        public string FirstName { get; set; }
        [StringLength(15)]
        public string LastName { get; set; }
        [MaxLength(1, ErrorMessage ="The gender is a single character that is 'M' or 'F'")]      
        public string Gender { get; set; }
        [StringLength(20)]
        public string Stack { get; set; }
    }
}
