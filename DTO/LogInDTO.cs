using System.ComponentModel.DataAnnotations;

namespace WebApiClass.DTO
{
    public class LogInDTO
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        

     
    }
}
