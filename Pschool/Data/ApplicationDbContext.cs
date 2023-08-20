using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Pschool.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        
        {
                
        }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
