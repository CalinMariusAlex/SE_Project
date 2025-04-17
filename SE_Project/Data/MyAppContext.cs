using Microsoft.EntityFrameworkCore;
using SE_Project.Models;

namespace SE_Project.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
