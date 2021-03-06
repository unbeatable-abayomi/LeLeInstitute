﻿using LeLeInstitute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Services.IRepository
{
   public interface IDepartmentRepository : IRepository<Department>
    {
        IEnumerable<Department> InStructorToDeparments();

        Department InstructorToDeparment(int id);
    }
}
