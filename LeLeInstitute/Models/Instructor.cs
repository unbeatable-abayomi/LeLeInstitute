using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       
        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:dd,MM,yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public OfficeAssignment OfficeAssignment { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }

    }
}
