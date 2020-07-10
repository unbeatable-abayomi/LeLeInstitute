using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(LeLeContext context, ICourseRepository courseRepository)
        {
           

            _courseRepository = courseRepository;
        }  
        
        
        
        public IActionResult Lists()
        {
            return View(_courseRepository.GetAll());
        }
        
        
        
        public IActionResult Index()
        {
            ////method syntax
            //var allCourse = _context.Courses.Include(d => d.Department).ToList();

            ////query syntax

            //var querySyntax = from dept in _context.Departments join course in _context.Courses on dept.DepartmentId equals course.DepartmentId select course;
           
            var allCourses = _courseRepository.CoursesToDeparment();
            return View(allCourses);
        }
    }
}
