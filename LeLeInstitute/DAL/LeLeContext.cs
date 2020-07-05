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
        public LeLeContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new DepartemntConfig());
        }

    }
}
