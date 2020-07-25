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

            builder.HasOne(navigationExpression: i => i.Instructor)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);
            builder.Property(s => s.StudentId).ValueGeneratedOnAdd();
            builder.Property(p => p.FirstName).HasColumnType("Nvarchar(50)");
            builder.Property(p => p.LastName).HasColumnType("Nvarchar(50)");
            builder.Property(p => p.EnrollmentDate).HasColumnType("Date").HasDefaultValueSql("GetDate()");

        }

    }


    public class OfficeAssignmentConfig : IEntityTypeConfiguration<OfficeAssignment>
    {
        public void Configure(EntityTypeBuilder<OfficeAssignment> builder)
        {
            builder.HasKey(e => e.InstructorId);
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

    public class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(e => e.InstructorId);
            builder.Property(p => p.FirstName).HasMaxLength(25);
            builder.Property(p => p.LastName).HasMaxLength(25);
            builder.Property(p => p.HireDate).HasColumnType("Date").HasDefaultValueSql("GetDate()");
            builder.Ignore(p => p.FullName);

            builder.HasOne(navigationExpression: o => o.OfficeAssignment)
              .WithOne(navigationExpression: i => i.Instructor)
              .HasForeignKey<OfficeAssignment>(i => i.InstructorId);

        }
    }

    public class CourseAssignmentConfig : IEntityTypeConfiguration<CourseAssignment>
    {
        public void Configure(EntityTypeBuilder<CourseAssignment> builder)
        {
            builder.HasKey(k => new { k.InstructorId,k.CourseId});
        


            builder.HasOne(navigationExpression: c => c.Instructor)
              .WithMany(navigationExpression: ca => ca.CourseAssignments)
              .HasForeignKey(i => i.InstructorId);



            builder.HasOne(navigationExpression: c => c.Course)
              .WithMany(navigationExpression: ca => ca.CourseAssignments)
              .HasForeignKey(i => i.CourseId);


        }
    }
}