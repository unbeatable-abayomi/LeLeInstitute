using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Display(Name ="Name")]
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }

        public int? InstructorId { get; set; }
        [Display(Name = "Adminstrator")]
        public Instructor Instructor { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
