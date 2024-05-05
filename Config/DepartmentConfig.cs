using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SchoolSystemCore.Models.Config;
public class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        builder.HasKey(t => t.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(n => n.DepartmentName).IsRequired().HasMaxLength(200);
        builder.Property(n => n.Description).IsRequired(false).HasMaxLength(500);


        builder.HasData(new List<Department>()
           {
               new Department{
                   Id = 1,
                   DepartmentName="IT",
                   Description="aaaaaaaaaaaaaa",

               },


               new Department{
                   Id = 2,
                   DepartmentName="BIO",
                   Description="bbbbbbbbbbbbbbbb",

               },
            });
    }
}
