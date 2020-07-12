using LeLeInstitute.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.DAL
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(k => k.CourseId);
            builder.Property(p => p.CourseId).ValueGeneratedOnAdd();
            builder.Property(p => p.Credits).IsRequired();
            builder.HasOne(navigationExpression: d => d.Department)
                .WithMany(navigationExpression: c => c.Courses)
                .HasForeignKey(f => f.DepartmentId);
        }
     
    }
    public class DepartemntConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(k => k.DepartmentId);
            builder.Property(p => p.DepartmentId).ValueGeneratedOnAdd();
            builder.Property(p => p.DepartmentName).IsRequired().HasColumnType("Nvarchar(50)");
            builder.Property(p => p.Budget).IsRequired();

            //or
            //builder.HasMany(navigationExpression: m => m.Courses)
            //  .WithOne(navigationExpression: o => o.Department)
            //  .HasForeignKey(f => f.DepartmentId);
        }

    }
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);
            builder.Property(p => p.FirstName).HasColumnType("Nvarchar(50)");
            builder.Property(p => p.LastName).HasColumnType("Nvarchar(50)");
            builder.Property(p => p.EnrollmentDate).HasColumnType("Date").HasDefaultValueSql("GetDate()");
        
        }

    }
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.EnrollmentId);
            builder.Property(p => p.Grade).IsRequired();
            
            
            builder.HasOne(navigationExpression: s => s.Student)
              .WithMany(navigationExpression: e => e.Enrollments)
              .HasForeignKey(s => s.StudentId);
           
            builder.HasOne(navigationExpression: c => c.Course)
           .WithMany(navigationExpression: e => e.Enrollments)
           .HasForeignKey(c => c.CourseId);
        }

    }
}
