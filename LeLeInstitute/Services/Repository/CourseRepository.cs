using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Services.Repository
{
    public class CourseRepository :Repository<Course>,ICourseRepository
    {
        public CourseRepository(LeLeContext leLeContext) : base(leLeContext)
        {

        }

        IEnumerable<Course> ICourseRepository.CoursesToDeparment()
        {
            return LeLeContext.Courses.Include(x => x.Department).ToList(); 
        }
    }
}
