using Microsoft.EntityFrameworkCore;
using WebApiClass.Model;

namespace WebApiClass.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
