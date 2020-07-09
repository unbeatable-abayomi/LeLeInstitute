using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Services
{
   public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> CoursesToDeparment();
    }
}
