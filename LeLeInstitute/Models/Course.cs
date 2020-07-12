using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Models
{
    public class Course
    {


        //[Key()]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        [Required(ErrorMessage ="Please fill the coures title ")]
        [Display(Name ="Course Title")]
        public string CourseName { get; set; }
        public int Credits { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
