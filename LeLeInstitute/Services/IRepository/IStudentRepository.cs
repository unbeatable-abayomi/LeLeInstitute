﻿using LeLeInstitute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Services.IRepository
{
  public interface IStudentRepository : IRepository<Student> 
    {
        IEnumerable<Enrollment> CoursesToStudent(int studentId);
    }
}
