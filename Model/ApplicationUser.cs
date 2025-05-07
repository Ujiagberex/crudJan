using Microsoft.AspNetCore.Identity;

namespace WebApiClass.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string fullname { get; set; }
    }
}
