using LeLeInstitute.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.DAL
{
    public class LeLeContext : DbContext
    {
        //public LeLeContext(DbContextOptions<LeLeContext>options):base(options)
        //{
                
        //}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments  { get; set; }
        public DbSet<Student> Students  { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors   { get; set; }
        public DbSet<CourseAssignment> CourseAssignments  { get; set; }
      
        public DbSet<OfficeAssignment> OfficeAssignments  { get; set; }
        public LeLeContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new DepartemntConfig());
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new EnrollmentConfig());
            modelBuilder.ApplyConfiguration(new InstructorConfig());
            modelBuilder.ApplyConfiguration(new CourseAssignmentConfig());
            modelBuilder.ApplyConfiguration(new OfficeAssignmentConfig());

        }

    }
}
