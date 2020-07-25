using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Services.Repository
{
    public class DepartmentRepository :Repository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(LeLeContext leLeContext) : base(leLeContext)
        {

        }

        public Department InstructorToDeparment(int id)
        {
            var qs = (from department in LeLeContext.Departments
                      join instructor in LeLeContext.Instructors on department.InstructorId equals instructor.InstructorId
                      select department).FirstOrDefault(x => x.DepartmentId == id);
            return qs;

            //or
           // LeLeContext.Departments.Include(x => x.Instructor).FirstOrDefault(x => x.DepartmentId == id);
        }

        public IEnumerable<Department> InStructorToDeparments()
        {
            return LeLeContext.Departments.Include(i => i.Instructor).ToList();
        }
    }
}
