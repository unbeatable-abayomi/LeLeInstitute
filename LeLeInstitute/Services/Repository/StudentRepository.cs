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
    public class StudentRepository : Repository<Student>,IStudentRepository
    {
        public StudentRepository(LeLeContext leLeContext) : base(leLeContext)
        {

        }
         
        public IEnumerable<Enrollment> CoursesToStudent(int studentId)
        {
            return LeLeContext.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(navigationPropertyPath: x => x.Student)
                .Include(navigationPropertyPath: x => x.Course)
                .ToList();
        }


        //IEnumerable<Enrollment> IStudentRepository.CoursesToStudent(int studentId)
        //{
        //    return LeLeContext.Enrollments.Where(e => e.StudentId == studentId).Include(x => x.Student).Include(x => x.Course).ToList();
        //}
    }
}
