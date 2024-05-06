using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolSystemCore.Models.Config;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(t => t.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(n => n.StudentName).IsRequired().HasMaxLength(250);
        builder.Property(n => n.Addresss).IsRequired(false).HasMaxLength(250);
        builder.Property(n => n.FileName).IsRequired(false);
        builder.Property(n => n.Email).IsRequired().HasMaxLength(250);
        builder.Property(n => n.DOB);

        builder.HasData(new List<Student>()
       {
           new Student{
               Id = 1,
               StudentName="Muhdin",
               Addresss="aa",
               Email="m@gmail.com",
               DOB=new DateTime(2022,12,12),

           },

           new Student{
               Id = 2,
               StudentName="Mussema",
               Addresss="aa",
               Email="ms@gmail.com",
               DOB=new DateTime(2022,12,12),

           }
        });

        builder.HasOne(d => d.Department).WithMany(n => n.Students).HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Students_Departments");
    }
}
