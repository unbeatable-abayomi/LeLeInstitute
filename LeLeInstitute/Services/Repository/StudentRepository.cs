using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Services.Repository
{
    public class StudentRepository : Repository<Student> , IStudentRepository
    {
        public StudentRepository(LeLeContext leLeContext) : base(leLeContext)
        {

        }
    }
}
