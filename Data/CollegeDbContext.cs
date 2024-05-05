using Microsoft.EntityFrameworkCore;
using SchoolSystemCore.Models.Config;
namespace SchoolSystemCore.Models;

public class CollegeDbContext : DbContext
{

    public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options)
    {

    }
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Table 1
        modelBuilder.ApplyConfiguration(new StudentConfig());
        modelBuilder.ApplyConfiguration(new DepartmentConfig());

        //Table 2
        //Table 3
    }
}
